
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

    /// <summary>
    /// the method returns as output a list of orders for manager view only. The orders in the  manager list include additional fields: number of order items, total order price and the order's status 
    /// </summary>
    /// <returns>IEnumerable<BO.OrderForList></returns>
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


    /// <summary>
    /// the method recieves as input order ID, and returs as output a (BO type) order for manager view only
    /// </summary>
    /// <param name="oID"></param>
    /// <returns>BO.Order</returns>
    /// <exception cref="IlegalDataException"></exception>
    public BO.Order GetByID(int oID)
    {
        try
        {
            if (oID < 0) // if order ID is a negative number then throw iligal data exception
                throw new BO.IlegalDataException("Order ID can't be a negative number");
            DO.Order order = Dal.Order.GetByID(oID);

            string oStatus = OrderStatus(order); // status of current order
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
                Status = (BO.Enums.OrderStatus)Enum.Parse(typeof(BO.Enums.OrderStatus), oStatus), //convert string to enum
                PaymentDate = order.OrderDate,
                ShipDate = order.ShipDate,
                DeliveryDate = order.DeliveryDate,
                Items = oi.ToList(),
                TotalPrice = oTotalPrice 
            }; // returning order of BO tipe
        }
        catch (DalApi.NotExistingException e) { throw e; } // if order or product dos not exist in data surce the throw not existing exception
    }

    /// <summary>
    /// the method receives as input: orders ID and returs as output: BO order after it has been updated by the manager to be shipped today 
    /// </summary>
    /// <param name="oID"></param>
    /// <returns>BO.Order</returns>
    /// <exception cref="FailedUpdatingObjectException"></exception>
    public BO.Order UpdateShipping(int oID)
    {
        try
        {
            if (oID < 0) // if order ID is a negative number then throw iligal data exception
                throw new BO.FailedUpdatingObjectException (new BO.IlegalDataException("Order ID can't can't be a negative number"));
            DO.Order dOrder = Dal.Order.GetByID(oID);
            if (dOrder.ShipDate != null && DateTime.Compare((DateTime)dOrder.ShipDate, DateTime.Now) <= 0) // checking if the order was already shipped. if so then throw status exception
                throw new BO.FailedUpdatingObjectException( new BO.ConflictingStatusException("The order has already been shipped"));

            dOrder.ShipDate = DateTime.Now; // updating the order shipping date in data surce
            Dal.Order.Update(dOrder);
            return GetByID(oID); // returns updated BO order
        }
        catch (DalApi.NotExistingException e) { throw new BO.FailedUpdatingObjectException(e); } // faild updating order because: order or product don't exist in data surce 
    }

    /// <summary>
    /// the method receives as input: orders ID and returs as output: BO order after it has been updated by the manager to be deliverd today.
    /// </summary>
    /// <param name="oID"></param>
    /// <returns>BO.Order</returns>
    /// <exception cref="FailedUpdatingObjectException"></exception>
    public BO.Order UpdateDelivery(int oID)
    {
        try
        {
            if (oID < 0) // if order ID is a negative number then throw iligal data exception
                throw new BO.FailedUpdatingObjectException(new BO.IlegalDataException("Order ID can't be a negative number"));
            DO.Order dOrder = Dal.Order.GetByID(oID);
            if (dOrder.ShipDate == null && dOrder.DeliveryDate == null) // chcking that dates are not null
                throw new BO.FailedUpdatingObjectException(new BO.ConflictingStatusException("The status of order is null"));
            if ((DateTime.Compare((DateTime)dOrder.ShipDate, DateTime.Now) > 0) || DateTime.Compare((DateTime)dOrder.DeliveryDate, DateTime.Now) <= 0)  // checking if the order was already shipped and not been deliverd yet. if not so then throw status exception
                throw new BO.FailedUpdatingObjectException(new BO.ConflictingStatusException("The order has not been shipped or has already been deliverd"));

            dOrder.DeliveryDate = DateTime.Now; // updating the order delivery date in data surce
            Dal.Order.Update(dOrder);
            return GetByID(oID); // returns updated BO order
        }
        catch (DalApi.NotExistingException e) { throw new BO.FailedUpdatingObjectException(e); } // faild updating order because: order or product don't exist in data surce
    }

    /// <summary>
    /// the method tracks order and returns tracking object that describes the order's tracking status
    /// </summary>
    /// <param name="oID"></param>
    /// <returns>BO.OrderTracking</returns>
    /// <exception cref="BO.FailedUpdatingObjectException"></exception>
    public BO.OrderTracking TrackOrder(int oID)
    {
        try
        {
            if (oID < 0) // if order ID is a negative number then throw iligal data exception
                throw new BO.FailedUpdatingObjectException(new BO.IlegalDataException("Order ID can't be a negative number"));
            DO.Order dOrder = Dal.Order.GetByID(oID);

            List<string> trackingLst = new List<string>(); // description list of the order status 
            if (dOrder.OrderDate != null)
                trackingLst.Add(dOrder.OrderDate.ToString() + ": order confirmed");
            if (dOrder.ShipDate != null)
                trackingLst.Add(dOrder.ShipDate.ToString() + ": order shipped");
            if (dOrder.DeliveryDate != null)
                trackingLst.Add(dOrder.DeliveryDate.ToString() + ": order deliverd");

            string oStatus = OrderStatus(dOrder); // status of current order
            return new BO.OrderTracking { 
                ID = dOrder.ID,
                Status = (BO.Enums.OrderStatus)Enum.Parse(typeof(BO.Enums.OrderStatus), oStatus), // convert string to enum
                TrackingStages = trackingLst.ToList() };
        }
        catch (DalApi.NotExistingException e) { throw new BO.FailedUpdatingObjectException(e); } // faild updating order because: order or product don't exist in data surce
    }

    /// <summary>
    /// the method aloows manager to update or delete amount of copies of product in a specific order
    /// </summary>
    /// <param name="oID"></param>
    /// <returns>BO.Order</returns>
    /// <exception cref="FailedUpdatingObjectException"></exception>
    public BO.Order ManagerUpdateOrder(int oID)
    {
        try
        {
            BO.Order o = GetByID(oID);
            if (o.ShipDate != null && DateTime.Compare((DateTime)o.ShipDate, DateTime.Now) <= 0) // if order has been shipped then throw exception
                throw new FailedUpdatingObjectException(new ConflictingStatusException("Order has already been shipped"));

            Console.WriteLine("Enter the product's ID that you want to update:");
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
                    Console.WriteLine($"Enter updated amount of product up to-{pAmount}");
                    int UpdatedAmount;
                    int.TryParse(Console.ReadLine(), out UpdatedAmount); // converts the input to integer
                    if (UpdatedAmount < 0 || UpdatedAmount > pAmount) // if the amount enterd is a negative number or a bigger number then the amount in stock then, throw ilegal datd exception
                        throw new IlegalDataException("entered invalid amount");

                    Dal.OrderItem.Update(new DO.OrderItem { // updating list of Order item (in data surce)
                        ID = oi.ID, ProductId = oi.ProductID,
                        OrderId = oID,
                        Price = oi.Price,
                        Amount = UpdatedAmount
                    });

                    p.InStock += oi.Amount - UpdatedAmount; // updating list of products (in data surce)
                    oi.Amount = UpdatedAmount; // updating order item (in BO)
                    oi.TotalPrice = UpdatedAmount * oi.Price;
                    o.TotalPrice += (UpdatedAmount - oi.Amount) * oi.Price; // updating order (in BO)
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
        if (o.DeliveryDate != null && DateTime.Compare((DateTime)o.DeliveryDate, DateTime.Now) <= 0)  // comparing delivery date  with today's date
            oStatus = "OrderDelivered";
        else if (o.ShipDate != null && DateTime.Compare((DateTime)o.ShipDate, DateTime.Now) <= 0)   // comparing Shiping date with today's date
            oStatus = "OrderShiped";
        else oStatus = "OrderConfirmed";
        return oStatus;
    }

}
