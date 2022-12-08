﻿
using BlApi;
using BO;
using System.ComponentModel.DataAnnotations;

namespace BlImplementation;

internal class Cart : ICart
{
    /// <summary>
    /// private field for allowing accsess from BL to Dal
    /// </summary>
    private DalApi.IDal Dal = new Dal.DalList();


    /// <summary>
    /// The method recieves as input a specific shopping cart and a product ID. The method identifing the product useing the Uniqe ID and adds it to cart, finally the method returns as output the updated shopping Cart.
    /// </summary>
    /// <param name="crt"></param>
    /// <param name="pID"></param>
    /// <returns>BO.cart</returns>
    /// <exception cref="FailedAddingObjectException"></exception>
    public BO.Cart AddToCart(BO.Cart crt, int pID)
    {
        try
        {

            DO.Product? p = Dal.Product.GetByID(pID); // finding product (that we're adding to cart) in products catalog 

            crt.Items ??= new List<BO.OrderItem?>();

            BO.OrderItem? orderItem = crt.Items.FirstOrDefault(item => item?.ID == pID);
            if (orderItem != null)
            {
                if (p?.InStock <= orderItem.Amount) // if there is not enough of the product (that we want to add) in stock then throw
                    throw new OutOfStockException(); // failed adding product to cart because: product to add is out of stock
                orderItem.Amount += 1;
                orderItem.TotalPrice += orderItem.Price;
                crt.TotalPrice += orderItem.Price;
            }
            //foreach (BO.OrderItem? oi in crt.Items!) // running on list of all items in cart
            //    if (oi?.ID == pID)
            //    {
            //        flag = true; // item already exists in cart
            //        if (p?.InStock <= oi.Amount) // if there is not enough of the product (that we want to add) in stock then throw
            //            throw new OutOfStockException(); // failed adding product to cart because: product to add is out of stock
            //        oi.Amount += 1;
            //        oi.TotalPrice += oi.Price;
            //        crt.TotalPrice += oi.Price;
            //        break;
            //    }

            else
            { 
                if (p?.InStock <= 0) // if product to add is out of stock then throw
                    throw new OutOfStockException(); // failed adding product to cart because: product to add is out of stock
                crt.Items.Add(new BO.OrderItem
                {
                    Name = p?.Name,
                    ProductID = pID,
                    Price = p?.Price ?? 0,
                    Amount = 1,
                    TotalPrice = p?.Price ?? 0

                }); // adding product to cart
                crt.TotalPrice += p?.Price ?? 0; // updating the shopping cart total price}
            }

            //if (!flag) // if the product to add does not exists in shopping cart and it is in stock then add the product to shopping cart
            //{
            //    if (p?.InStock <= 0) // if product to add is out of stock then throw
            //        throw new OutOfStockException(); // failed adding product to cart because: product to add is out of stock
            //    if (crt.Items.Count() == 0) // if List of order Items is empty.
            //        crt.Items = new List<BO.OrderItem?>();
            //    crt.Items.Add(new BO.OrderItem
            //    {
            //        Name = p?.Name,
            //        ProductID = pID,
            //        Price = p?.Price??0,
            //        Amount = 1,
            //        TotalPrice = p?.Price??0

            //    }); // adding product to cart
            //    crt.TotalPrice += p.Value.Price; // updating the shopping cart total price}
            //}
            return crt;
        }
        catch (Exception ex) { throw new FailedAddingObjectException(ex); } // failed adding product to cart because: product to add does not exist in catalog
    }


    /// <summary>
    /// The method recieves as input a specific shopping cart, an order item ID and an amount to update. The method identifing the order item (from shopping cart) useing the Uniqe ID and updats the amount of this specific order item in cart, finally the method returns as output the updated shopping Cart.
    /// </summary>
    /// <param name="crt"></param>
    /// <param name="pID"></param>
    /// <param name="amount"></param>
    /// <returns>BO.Cart</returns>
    /// <exception cref="FailedUpdatingObjectException"></exception>
    public BO.Cart UpdateAmountInCart(BO.Cart crt, int pID, int amount)
    {
        try
        {
            DO.Product? p = Dal.Product.GetByID(pID); // finding product (that we're adding to cart) in products catalog
            if (amount < 0 || amount > p?.InStock) // if amount is negative or if there is not enough of product in stock  then throw message
                throw new IlegalDataException("Ilegal amount"); // failed updating product in cart beacuse of ilega data 

            if (crt.Items?.Count == 0) // if cart is empty then throw not existing ecxeption
                throw new DalApi.NotExistingException();

            bool flag = false;
            foreach (BO.OrderItem? oi in crt.Items!) // running on list of all items in cart
                if (oi?.ProductID == pID) // if product was found then update data
                {
                    flag = true;
                    crt.TotalPrice += (amount - oi.Amount) * oi.Price;
                    if (amount != 0)
                    {
                        oi.TotalPrice += (amount - oi.Amount) * oi.Price;
                        oi.Amount = amount;
                    }
                    else // if amount is zero then delete item from cart
                        crt.Items.Remove(oi);
                    break;
                }
            if (!flag) // if order item was not found in shopping cart then throw not existing exception
                throw new DalApi.NotExistingException();
            return crt;

        }
        catch (Exception ex) { throw new FailedUpdatingObjectException(ex); } // failed updating product to cart because: product to update does not exist in catalog 
    }


    public int OrderCart(BO.Cart crt)
    {
        try
        {
            if (crt.Items?.Count == 0) // if cart is empty then throw not existing ecxeption
                throw new DalApi.NotExistingException();

            DO.Product? dproduct;
            foreach (BO.OrderItem? item in crt.Items!)
            {
                dproduct = Dal.Product.GetByID(item?.ProductID ?? 0); // finding product (that we're adding to cart) in products catalog
                if (item?.Amount <= 0)  // if the amount of product is negative then throw message
                    throw new IlegalDataException("Ilegal amount of products");
                if (dproduct?.InStock < item?.Amount) // if the amount of a specific order item in shopping cart is bigger then the amount of that specific product in stock then throw message
                    throw new OutOfStockException();
            }

            if (!new EmailAddressAttribute().IsValid(crt.CustomerEmail))
                throw new IlegalDataException("Ilegal email");

            int oID = Dal.Order.Add(new DO.Order
            {
                CustomerName = crt.CustomerName,
                CustomerEmail = crt.CustomerEmail,
                CustomerAdress = crt.CustomerAdress,
                OrderDate = DateTime.Now,
                ShipDate = null,
                DeliveryDate = null,
            }); // adding current order to order list in data surce.

            foreach (BO.OrderItem? item in crt.Items) // creating order item objects According to product's in shopping cart and adding them to  order item list in data surce
            {
                Dal.OrderItem.Add(new DO.OrderItem
                {
                    ProductId = item?.ProductID ?? 0,
                    OrderId = oID,
                    Price = item?.Price ?? 0,
                    Amount = item?.Amount ?? 0,
                }); // adding current order item to order item list in data surce.
                dproduct = Dal.Product.GetByID(item?.ProductID ?? 0);


                Dal.Product.Update(new DO.Product()
                {
                    ID = item?.ProductID ?? 0,
                    Name = dproduct?.Name,
                    Category = dproduct?.Category,
                    Price = dproduct?.Price ?? 0,
                    InStock = dproduct?.InStock ?? 0 - item?.Amount ?? 0,// updating stock in data surce after the order has been confirmed 
                });
            }
            return oID;
        }
        catch (Exception e) { throw new FailedToConfirmOrderException(e); } // failed updating product to cart because: product to update does not exist in catalog

    }

}



