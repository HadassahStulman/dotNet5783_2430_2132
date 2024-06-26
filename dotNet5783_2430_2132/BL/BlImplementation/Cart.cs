﻿
using BlApi;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace BlImplementation;

internal class Cart : ICart
{
    /// <summary>
    /// private field for allowing accsess from BL to Dal
    /// </summary>
    private DalApi.IDal Dal = DalApi.Factory.Get()!;

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
            DO.Product? p = Dal.Product.GetIf(item => (item?.ID) == pID); // finding product (that we're adding to cart) in products catalog 

            crt.Items ??= new ObservableCollection<BO.OrderItem?>();

            BO.OrderItem? orderItem = crt.Items.FirstOrDefault(item => (item?.ID ?? 0) == pID);
            if (orderItem != null)
            {
                if (p?.InStock <= orderItem.Amount) // if there is not enough of the product (that we want to add) in stock then throw
                    throw new BO.OutOfStockException(); // failed adding product to cart because: product to add is out of stock
                orderItem.Amount += 1;
                orderItem.TotalPrice += orderItem.Price;
                crt.TotalPrice += orderItem.Price;
            }

            else
            {
                if (p?.InStock <= 0) // if product to add is out of stock then throw
                    throw new BO.OutOfStockException(); // failed adding product to cart because: product to add is out of stock
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

            return crt;
        }
        catch (Exception ex) { throw new BO.FailedAddingObjectException(ex); } // failed adding product to cart because: product to add does not exist in catalog
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
            DO.Product? p = Dal.Product.GetIf(item => (item?.ID ?? 0) == pID); // finding product (that we're adding to cart) in products catalog
            if (amount < 0) // if amount is negative or if there is not enough of product in stock  then throw message
                throw new BO.IlegalDataException("Ilegal amount"); // failed updating product in cart beacuse of ilega data 
            if (amount > p?.InStock)
                throw new BO.OutOfStockException();
            if (crt.Items?.Count == 0) // if cart is empty then throw not existing ecxeption
                throw new BO.IlegalDataException("Cart Is Empty");

            var item = crt?.Items!.ToList().Find(item => item?.ProductID == pID);
            if (item == null)
                throw new DO.NotExistingException();
            crt!.TotalPrice += (amount - item.Amount) * item.Price;
            item.Amount = amount;
            crt?.Items!.Remove(item);
            if (amount != 0)
            {
                item.TotalPrice += (amount - item.Amount) * item.Price;
                crt?.Items!.Add(item);
            }
            return crt!;

        }
        catch (Exception ex) { throw new BO.FailedUpdatingObjectException(ex); } // failed updating product to cart because: product to update does not exist in catalog 
    }


    /// <summary>
    /// order all products in cart - create a new order and order items for all products
    /// </summary>
    /// <param name="crt"></param>
    /// <returns></returns>
    /// <exception cref="DO.NotExistingException"></exception>
    /// <exception cref="IlegalDataException"></exception>
    /// <exception cref="OutOfStockException"></exception>
    /// <exception cref="FailedToConfirmOrderException"></exception>
    public int OrderCart(BO.Cart crt)
    {
        try
        {
            if (crt.Items?.Count == 0) // if cart is empty then throw not existing ecxeption
                throw new DO.NotExistingException();

            DO.Product? dproduct;
            foreach (BO.OrderItem? orderItem in crt.Items!)
            {
                dproduct = Dal.Product.GetIf(item => (item?.ID ?? 0) == (orderItem?.ProductID ?? 1)); // finding product (that we're adding to cart) in products catalog
                if (orderItem?.Amount <= 0)  // if the amount of product is negative then throw message
                    throw new BO.IlegalDataException("Ilegal amount of products");
                if (dproduct?.InStock < orderItem?.Amount) // if the amount of a specific order item in shopping cart is bigger then the amount of that specific product in stock then throw message
                    throw new BO.OutOfStockException();
            }

            if (!new EmailAddressAttribute().IsValid(crt.CustomerEmail))
                throw new BO.IlegalDataException("Ilegal email");

            int oID = Dal.Order.Add(new DO.Order
            {
                CustomerName = crt.CustomerName,
                CustomerEmail = crt.CustomerEmail,
                CustomerAddress = crt.CustomerAddress,
                OrderDate = DateTime.Now,
                ShipDate = null,
                DeliveryDate = null,
            }); // adding current order to order list in data surce.

            foreach (BO.OrderItem? orderItem in crt.Items) // creating order item objects According to product's in shopping cart and adding them to  order item list in data surce
            {
                Dal.OrderItem.Add(new DO.OrderItem
                {
                    ProductId = orderItem?.ProductID ?? 0,
                    OrderId = oID,
                    Price = orderItem?.Price ?? 0,
                    Amount = orderItem?.Amount ?? 0,
                }); // adding current order item to order item list in data surce.
                dproduct = Dal.Product.GetIf(item => (item?.ID ?? 0) == (orderItem?.ProductID ?? 1));

                Dal.Product.Update(new DO.Product()
                {
                    ID = orderItem?.ProductID ?? 0,
                    Name = dproduct?.Name,
                    Category = dproduct?.Category,
                    Price = dproduct?.Price ?? 0,
                    InStock = dproduct?.InStock ?? 0 - orderItem?.Amount ?? 0,// updating stock in data surce after the order has been confirmed 
                });
            }
            return oID;
        }
        catch (Exception e) { throw new BO.FailedToConfirmOrderException(e); } // failed updating product to cart because: product to update does not exist in catalog

    }

}



