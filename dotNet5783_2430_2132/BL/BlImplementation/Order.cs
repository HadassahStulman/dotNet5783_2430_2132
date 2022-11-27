
using BlApi;
using BO;
using DO;
using System.Collections.Generic;
using static BO.Enums;

namespace BlImplementation;

internal class Order : IOrder
{
    /// <summary>
    /// private field for allowing accsess from BL to Dal
    /// </summary>
    private DalApi.IDal Dal = new Dal.DalList();


    public IEnumerable<BO.OrderForList> GetList()
    {
        
        IEnumerable<DO.Order> oLst = Dal.Order.GetList(); // list of orders from data surce
        List<BO.OrderForList> ofl = new List<BO.OrderForList>();

        foreach (DO.Order order in oLst) // building a new list (OrderForList) based on order's list data
        {
            try
            {
                string oStatus = OrderStatus(order); // status of current order
                double? oTotalPrice = 0;
                int? oAmount = 0;
                IEnumerable<DO.OrderItem> oiLst = Dal.OrderItem.GetAllItemsInOrder(order.ID);
                foreach (DO.OrderItem oiItem in oiLst) // calculating the amont of products and the total price of current order
                {
                    oAmount += oiItem.Amount;
                    oTotalPrice += oiItem.Price * oiItem.Amount;
                }

                ofl.Add(new BO.OrderForList { 
                    ID = order.ID,
                    CustomerName = order.CustomerName,
                    Status = (BO.Enums.OrderStatus)Enum.Parse(typeof(BO.Enums.OrderStatus), oStatus),
                    AmountOfItems = oAmount,
                    TotalPrice = oTotalPrice }); // adding order to list of- OrderForList
            }
            catch (DalApi.NotExistingException) { }; // if order has 0 items then don't add it to OrderForList
        }
        return ofl;

    }

  
    public BO.Order GetByID(int oID)
    {
        try
        {
            if (oID < 100000) // if order ID is ilegal then throw iligal data exception
                throw new BO.IlegalDataException(" Ilegal order ID");
            DO.Order order = Dal.Order.GetByID(oID);

            List<BO.OrderItem> oi = new List<BO.OrderItem>(); // creating new list for order items from BO
            int? oAmount = 0; // counter for calculating the amont of products in order
            double? oTotalPrice = 0;// counter for calculating the total price of order
            IEnumerable<DO.OrderItem> oiLst = Dal.OrderItem.GetAllItemsInOrder(order.ID); // list of order items from data surce
            foreach (DO.OrderItem oiItem in oiLst) 
            {
                oAmount += oiItem.Amount; 
                oTotalPrice += oiItem.Price * oiItem.Amount; 

                DO.Product p = Dal.Product.GetByID(oiItem.ProductId); // getting prodct by ID from data surce, in order to have the name of order item
                oi.Add(new BO.OrderItem { 
                    ID = oiItem.ID,
                    Name = p.Name,
                    ProductID = oiItem.ProductId,
                    Price = oiItem.Price,
                    Amount = oiItem.Amount,
                    TotalPrice = (oiItem.Price * oiItem.Amount) 
                }); // adding order item to list of order Items in BO

            }

            return new BO.Order {
                ID = oID,
                CustomerName = order.CustomerName,
                CustomerEmail = order.CustomerEmail,
                CustomerAdress = order.CustomerAdress,
                OrderDate = order.OrderDate,
                Status = (BO.Enums.OrderStatus)Enum.Parse(typeof(BO.Enums.OrderStatus), OrderStatus(order)), //convert string to enum
                PaymentDate = order.OrderDate,
                ShipDate = order.ShipDate,
                DeliveryDate = order.DeliveryDate,
                Items = oi.ToList(),
                TotalPrice = oTotalPrice 
            }; // returning order of BO tipe
        }
        catch (DalApi.NotExistingException e) { throw e; } // if order or product dos not exist in data surce the throw not existing exception
    }


    public BO.Order UpdateShipping(int oID)
    {
        try
        {
            if (oID < 10000) // if order ID is a negative number then throw iligal data exception
                throw new BO.FailedUpdatingObjectException (new BO.IlegalDataException("Ilegal order ID"));
            DO.Order dOrder = Dal.Order.GetByID(oID);
            if (dOrder.ShipDate != DateTime.MinValue ) // checking if the order was already shipped. if so then throw status exception
                throw new BO.FailedUpdatingObjectException( new BO.ConflictingStatusException("The order has already been shipped"));

            dOrder.ShipDate = DateTime.Now; // updating the order shipping date in data surce
            Dal.Order.Update(dOrder);
            return GetByID(oID); // returns updated BO order
        }
        catch (DalApi.NotExistingException e) { throw new BO.FailedUpdatingObjectException(e); } // faild updating order because: order or product don't exist in data surce 
    }

    public BO.Order UpdateDelivery(int oID)
    {
        try
        {
            if (oID < 10000) // if order ID is a negative number then throw iligal data exception
                throw new BO.FailedUpdatingObjectException(new BO.IlegalDataException("Ilegal Order Id"));
            DO.Order dOrder = Dal.Order.GetByID(oID);
            if (dOrder.ShipDate == DateTime.MinValue) // if order has not been shipped then throw status exception
                throw new BO.FailedUpdatingObjectException(new BO.ConflictingStatusException("Order was not shipped Yet"));
            if (dOrder.DeliveryDate!= DateTime.MinValue) // if order was already deliverd then throw status exception
                throw new BO.FailedUpdatingObjectException(new BO.ConflictingStatusException("Order was already delivered"));

            dOrder.DeliveryDate = DateTime.Now; // updating the order delivery date in data surce
            Dal.Order.Update(dOrder);
            return GetByID(oID); // returns updated BO order
        }
        catch (DalApi.NotExistingException e) { throw new BO.FailedUpdatingObjectException(e); } // faild updating order because: order or product don't exist in data surce
    }

  
    public BO.OrderTracking TrackOrder(int oID)
    {
        try
        {
            if (oID < 100000) // if order ID is ilegal number then throw iligal data exception
                throw new BO.FailedUpdatingObjectException(new BO.IlegalDataException("Order ID is Ilegal"));
            DO.Order dOrder = Dal.Order.GetByID(oID);

            List<Tuple<DateTime, string>> trackingLst = new List<Tuple< DateTime, string>>(); // description list of the order status 
            trackingLst.Add(new Tuple<DateTime, string>( (DateTime)dOrder.OrderDate , "Order was Confirmed"));
            if (dOrder.ShipDate != DateTime.MinValue)
                trackingLst.Add(new Tuple<DateTime, string>( (DateTime)dOrder.ShipDate, "Order was Shipped"));
            if (dOrder.DeliveryDate != DateTime.MinValue)
                trackingLst.Add(new Tuple<DateTime, string>( (DateTime)dOrder.DeliveryDate , "Order was Delivered"));
            

            string oStatus = OrderStatus(dOrder); // status of current order
            return new BO.OrderTracking { 
                ID = dOrder.ID,
                Status = (BO.Enums.OrderStatus)Enum.Parse(typeof(BO.Enums.OrderStatus), oStatus), // convert string to enum
                TrackingStages = trackingLst};
        }
        catch (DalApi.NotExistingException e) { throw new BO.FailedUpdatingObjectException(e); } // faild updating order because: order or product don't exist in data surce
    }

   
    public BO.Order ManagerUpdateOrder(int oID)
    {
        try
        {
            BO.Order o = GetByID(oID);
            if (o.ShipDate != DateTime.MinValue) // if order has been shipped then throw exception status exception
                throw new FailedUpdatingObjectException(new ConflictingStatusException("Order has already been shipped"));

            Console.WriteLine("Enter ID of product that you want to update:");
            int pID;
            int.TryParse(Console.ReadLine(), out pID); // converts the input to integer

            if (o.Items == null) // if there are no order items in order then throw not existing exception
                throw new DalApi.NotExistingException();
            bool flag = false;
            foreach (BO.OrderItem oi in o.Items)
            {
                if (oi.ProductID == pID) 
                {
                    flag = true;
                    DO.Product p = Dal.Product.GetByID(pID); 
                    int? pAmount = p.InStock + oi.Amount; // max amount in stock
                    Console.WriteLine($"Enter products updated amount, up to-{pAmount}");
                    int UpdatedAmount;
                    int.TryParse(Console.ReadLine(), out UpdatedAmount); // converts the input to integer
                    if (UpdatedAmount < 0 || UpdatedAmount > pAmount) // if the amount enterd is a negative number or a bigger number then the amount in stock then, throw ilegal datd exception
                        throw new IlegalDataException("entered invalid amount");
                    if (UpdatedAmount == 0) // if amont to update is zero 
                    {
                        o.Items.Remove(oi); // remove item from order
                        Dal.OrderItem.Delete((int)oi.ID); // deleting item from order list (in data surce) 
                    }
                    else Dal.OrderItem.Update(new DO.OrderItem
                    { 
                        ID = oi.ID,
                        ProductId = oi.ProductID,
                        OrderId = oID,
                        Price = oi.Price,
                        Amount = UpdatedAmount
                    }); // updating list of Order item (in data surce)

                    p.InStock += oi.Amount - UpdatedAmount; // updateing stock of products
                    Dal.Product.Update(p); // updating list of products (in data surce)
                    //oi.Amount = UpdatedAmount; // updating order item (in BO)
                    //oi.TotalPrice = UpdatedAmount * oi.Price;
                    //o.TotalPrice += (UpdatedAmount - oi.Amount) * oi.Price; // updating order (in BO)
                    break;
                }
            }
            if (!flag) // if order item doesn't exist in order then throw- not existing exception
                throw new FailedUpdatingObjectException(new DalApi.NotExistingException());
            return o;
            
        }
        catch (IlegalDataException e) { throw new FailedUpdatingObjectException(e); } // if order ID is a negative number then throw iligal data exception
        catch (DalApi.NotExistingException e) { throw new FailedUpdatingObjectException(e); } // faild updating order because: order or product don't exist in data surce 
    }

    /// <summary>
    ///  the method receives as input an order and returns its status
    /// </summary>
    /// <param name="o"></param>
    /// <returns>string</returns>
    public string OrderStatus(DO.Order o)
    {
        string oStatus; // status of current order
        if (o.DeliveryDate != DateTime.MinValue) // checking if order has been deliverd
            oStatus = "OrderDelivered";
        else if (o.ShipDate != DateTime.MinValue) // checking if order has been shipped
            oStatus = "OrderShipped";
        else oStatus = "OrderConfirmed";
        return oStatus;
    }

}
