
using BlApi;
using BO;


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
            bool flag = false;
            DO.Product p = Dal.Product.GetByID(pID); // finding product (that we're adding to cart) in products catalog 

            if (crt.Items.Count() != 0)  // checking that cart is not empty
            {
                foreach (BO.OrderItem oi in crt.Items) // running on list of all items in cart
                    if (oi.ID == pID)
                    {
                        flag = true; // item already exists in cart
                        if (p.InStock <= oi.Amount) // if there is not enough of the product (that we want to add) in stock then throw
                            throw new OutOfStockException(); // failed adding product to cart because: product to add is out of stock
                        oi.Amount += 1;
                        oi.TotalPrice += oi.Price;
                        crt.TotalPrice += oi.Price;
                        break;
                    }
            }

            if (!flag) // if the product to add does not exists in shopping cart and it is in stock then add the product to shopping cart
            {
                if (p.InStock <= 0) // if product to add is out of stock then throw
                    throw new OutOfStockException(); // failed adding product to cart because: product to add is out of stock
                if (crt.Items.Count() == 0) // if List of order Items is empty.
                    crt.Items = new List<BO.OrderItem>();
                crt.Items.Add(new BO.OrderItem
                {
                    Name = p.Name,
                    ProductID = pID,
                    Price = p.Price,
                    Amount = 1,
                    TotalPrice = p.Price

                }); // adding product to cart
                crt.TotalPrice += p.Price; // updating the shopping cart total price}
            }
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
            DO.Product p = Dal.Product.GetByID(pID); // finding product (that we're adding to cart) in products catalog
            if (amount < 0 || amount > p.InStock) // if amount is negative or if there is not enough of product in stock  then throw message
                throw new IlegalDataException("Ilegal amount"); // failed updating product in cart beacuse of ilega data 

            if (crt.Items.Count() == 0) // if cart is empty then throw not existing ecxeption
                throw new DalApi.NotExistingException();

            bool flag = false;
            foreach (BO.OrderItem oi in crt.Items) // running on list of all items in cart
                if (oi.ProductID == pID) // if product was found then update data
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


    public int? OrderCart(BO.Cart crt)
    {
        try
        {
            if (crt.Items.Count() == 0) // if cart is empty then throw not existing ecxeption
                throw new DalApi.NotExistingException();

            DO.Product p;
            foreach (BO.OrderItem item in crt.Items)
            {
                p = Dal.Product.GetByID(item.ProductID); // finding product (that we're adding to cart) in products catalog
                if (item.Amount <= 0)  // if the amount of product is negative then throw message
                    throw new IlegalDataException("Ilegal amount of products");
                if (p.InStock < item.Amount) // if the amount of a specific order item in shopping cart is bigger then the amount of that specific product in stock then throw message
                    throw new OutOfStockException();
            }

            if (!crt.CustomerEmail.Contains("@")) 
                throw new IlegalDataException("Ilegal email");

            int? oID = Dal.Order.Add(new DO.Order
            {
                CustomerName = crt.CustomerName,
                CustomerEmail = crt.CustomerEmail,
                CustomerAdress = crt.CustomerAdress,
                OrderDate = DateTime.Now,
                ShipDate = DateTime.MinValue,
                DeliveryDate = DateTime.MinValue,
            }); // adding current order to order list in data surce.

            foreach (BO.OrderItem item in crt.Items) // creating order item objects According to product's in shopping cart and adding them to  order item list in data surce
            {
                Dal.OrderItem.Add(new DO.OrderItem
                {
                    ProductId = item.ProductID,
                    OrderId = oID,
                    Price = item.Price,
                    Amount = item.Amount,
                }); // adding current order item to order item list in data surce.
                p = Dal.Product.GetByID(item.ProductID);
                p.InStock -= item.Amount; // updating stock in data surce after the order has been confirmed  
                Dal.Product.Update(p);
            }
            return oID;
        }
        catch (Exception e) { throw new FailedToConfirmOrderException(e); } // failed updating product to cart because: product to update does not exist in catalog

    }

}



