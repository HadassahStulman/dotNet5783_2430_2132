
using BlApi;


namespace BlImplementation;

internal class Order : IOrder
{
    /// <summary>
    /// private field for allowing accsess from BL to Dal
    /// </summary>
    private DalApi.IDal Dal = DalApi.Factory.Get()!;


    public IEnumerable<BO.OrderForList> GetList(Func<BO.OrderForList?, bool>? condition)
    {

        /* IEnumerable<DO.Order?> oLst = Dal.Order.GetList(); */// list of orders from data surce
                                                                //List<BO.OrderForList> ofl = new List<BO.OrderForList>();
        try
        {
            var ofllst =
                from order in Dal.Order.GetList()
                let oStatus = OrderStatus(order) // status of current order
                let oiLst = Dal.OrderItem.GetList(item => item?.OrderId == order?.ID)
                let oAmount = oiLst.Sum(oi => oi?.Amount ?? 0)
                let oTotalPrice = oiLst.Sum(oi => oi?.Price ?? 0 * oi?.Amount ?? 0)
                orderby order?.OrderDate
                select new BO.OrderForList
                {
                    ID = order?.ID ?? 0,
                    CustomerName = order?.CustomerName,
                    Status = (BO.Enums.OrderStatus)Enum.Parse(typeof(BO.Enums.OrderStatus), oStatus),
                    AmountOfItems = oAmount,
                    TotalPrice = oTotalPrice
                }; // adding order to list of- OrderForList
            return ofllst.AsEnumerable().Where(order => condition is null ? true : condition(order));
        }
        catch (Exception ex) { throw new BO.FailedGettingObjectException(ex); }; // if order has 0 items then don't add it to OrderForList

        //foreach (DO.Order? order in oLst) // building a new list (OrderForList) based on order's list data
        //{
        //    try
        //    {
        //        string oStatus = OrderStatus(order); // status of current order
        //        double oTotalPrice = 0;
        //        int oAmount = 0;
        //        IEnumerable<DO.OrderItem?> oiLst = Dal.OrderItem.GetList(item => item?.OrderId == order?.ID);
        //        oAmount = oiLst.Sum(oi => oi?.Amount ?? 0);
        //        oTotalPrice = oiLst.Sum(oi => oi?.Price ?? 0 * oi?.Amount ?? 0);
        //        ofl.Add(new BO.OrderForList
        //        {
        //            ID = order?.ID ?? 0,
        //            CustomerName = order?.CustomerName,
        //            Status = (BO.Enums.OrderStatus)Enum.Parse(typeof(BO.Enums.OrderStatus), oStatus),
        //            AmountOfItems = oAmount,
        //            TotalPrice = oTotalPrice
        //        }); // adding order to list of- OrderForList
        //    }
        //    catch (Exception ex) { throw new BO.FailedGettingObjectException(ex); }; // if order has 0 items then don't add it to OrderForList
        //}

    }

    public BO.Order GetByID(int oID)
    {
        try
        {
            if (oID < 100000) // if order ID is ilegal then throw iligal data exception
                throw new BO.IlegalDataException("Ilegal order ID");
            /*List<BO.OrderItem> BOoiLst = new List<BO.OrderItem>();*/ // creating new list for order items from BO
            IEnumerable<DO.OrderItem?> DALoiLst = Dal.OrderItem.GetList(item => item?.OrderId == oID); // list of order items from data surce
            var BOoiLst =
                from orderItem in DALoiLst
                let p = Dal.Product.GetIf(item => item?.ID == orderItem?.ProductId)
                select new BO.OrderItem
                {
                    ID = orderItem?.ID ?? 0,
                    Name = p?.Name,
                    ProductID = orderItem?.ProductId ?? 0,
                    Price = orderItem?.Price ?? 0,
                    Amount = orderItem?.Amount ?? 0,
                    TotalPrice = (orderItem?.Price ?? 0) * (orderItem?.Amount ?? 0)
                };
            int oAmount = DALoiLst.Sum(ordetItem => ordetItem?.Amount ?? 0);
            double oTotalPrice = DALoiLst.Sum(ordetItem => (ordetItem?.Price ?? 0) * (ordetItem?.Amount ?? 0));
            //foreach (DO.OrderItem? oiItem in DALoiLst)
            //{
            //    oAmount += (int)oiItem?.Amount!;
            //    oTotalPrice += (int)oiItem?.Price! * (int)oiItem?.Amount!;

            //    DO.Product? p = Dal.Product.GetIf(item => item?.ID == oiItem?.ProductId); // getting prodct by ID from data surce, in order to have the name of order item
            //    BOoiLst.Add(new BO.OrderItem
            //    {
            //        ID = oiItem?.ID ?? 0,
            //        Name = p?.Name,
            //        ProductID = oiItem?.ProductId ?? 0,
            //        Price = oiItem?.Price ?? 0,
            //        Amount = oiItem?.Amount ?? 0,
            //        TotalPrice = (oiItem?.Price ?? 0) * (oiItem?.Amount ?? 0)
            //    }); // adding order item to list of order Items in BO
            //}
            DO.Order? order = Dal.Order.GetIf(item => (item?.ID ?? 0) == oID);
            return new BO.Order
            {
                ID = oID,
                CustomerName = order?.CustomerName,
                CustomerEmail = order?.CustomerEmail,
                CustomerAdress = order?.CustomerAdress,
                OrderDate = order?.OrderDate,
                Status = (BO.Enums.OrderStatus)Enum.Parse(typeof(BO.Enums.OrderStatus), OrderStatus(order)), //convert string to enum
                PaymentDate = order?.OrderDate,
                ShipDate = order?.ShipDate,
                DeliveryDate = order?.DeliveryDate,
                Items = BOoiLst.ToList(),
                TotalPrice = oTotalPrice
            }; // returning order of BO type
        }
        catch (Exception ex) { throw new BO.FailedGettingObjectException(ex); } // if order or product dos not exist in data surce the throw not existing exception
    }


    public BO.Order UpdateShipping(int oID)
    {

       // BO.OrderTracking o = new();
       // o.TrackingStages.First().Item1;
        try
        {
            if (oID < 10000) // if order ID is a negative number then throw iligal data exception
                throw new BO.IlegalDataException("Ilegal order ID");
            DO.Order? dOrder = Dal.Order.GetIf(item => item?.ID == oID);
            if (dOrder?.ShipDate != null) // checking if the order was already shipped. if so then throw status exception
                throw new BO.ConflictingStatusException("The order has already been shipped");
            Dal.Order.Update(new DO.Order()
            {
                ID = dOrder?.ID ?? 0,
                CustomerName = dOrder?.CustomerName,
                CustomerAdress = dOrder?.CustomerAdress,
                CustomerEmail = dOrder?.CustomerEmail,
                OrderDate = dOrder?.OrderDate,
                ShipDate = DateTime.Now, // updating the order shipping date in data surce
                DeliveryDate = null
            });
            return GetByID(oID); // returns updated BO order
        }
        catch (Exception ex) { throw new BO.FailedUpdatingObjectException(ex); } // faild updating order because: order or product don't exist in data surce 
    }

    public BO.Order UpdateDelivery(int oID)
    {
        try
        {
            if (oID < 10000) // if order ID is a negative number then throw iligal data exception
                throw new BO.IlegalDataException("Ilegal Order Id");

            DO.Order? dOrder = Dal.Order.GetIf(item => item?.ID == oID);
            if (dOrder == null)
                throw new BO.FailedUpdatingObjectException(new DO.NotExistingException());

            if (dOrder?.ShipDate == null) // if order wasn't shipped then throw status exception
                throw new BO.ConflictingStatusException("Order was not shipped Yet");

            if (dOrder?.DeliveryDate != null) // if order was already deliverd then throw status exception
                throw new BO.ConflictingStatusException("Order was already delivered");

            Dal.Order.Update(new DO.Order() // updates delivery date in order list
            {
                ID = dOrder?.ID ?? 0,
                CustomerName = dOrder?.CustomerName,
                CustomerAdress = dOrder?.CustomerAdress,
                CustomerEmail = dOrder?.CustomerEmail,
                OrderDate = dOrder?.OrderDate,
                ShipDate = dOrder?.ShipDate, // updating the order shipping date in data surce
                DeliveryDate = DateTime.Now
            });
            return GetByID(oID); // returns updated BO order
        }
        catch (Exception ex) { throw new BO.FailedUpdatingObjectException(ex); } // faild updating order because: order or product don't exist in data surce
    }


    public BO.OrderTracking TrackOrder(int oID)
    {
        try
        {
            if (oID < 100000) // if order ID is ilegal number then throw iligal data exception
                throw new BO.FailedUpdatingObjectException(new BO.IlegalDataException("Ilegal Order ID "));
            DO.Order? dOrder = Dal.Order.GetIf(item => item?.ID == oID);
            if (dOrder == null)
                throw new BO.FailedToTrackOrderException(new DO.NotExistingException());
            List<Tuple<DateTime?, string?>> trackingLst = new List<Tuple<DateTime?, string?>>(); // description list of the order status 
            trackingLst.Add(new Tuple<DateTime?, string?>(dOrder?.OrderDate, "Order was Confirmed"));

            if (dOrder?.ShipDate != null)
                trackingLst.Add(new Tuple<DateTime?, string?>(dOrder?.ShipDate, "Order was Shipped"));

            if (dOrder?.DeliveryDate != null)
                trackingLst.Add(new Tuple<DateTime?, string?>(dOrder?.DeliveryDate, "Order was Delivered"));

            string oStatus = OrderStatus(dOrder); // status of current order
            return new BO.OrderTracking
            {
                ID = dOrder?.ID ?? 0,
                Status = (BO.Enums.OrderStatus)Enum.Parse(typeof(BO.Enums.OrderStatus), oStatus), // convert string to enum
                TrackingStages = trackingLst
            };
        }
        catch (Exception ex) { throw new BO.FailedToTrackOrderException(ex); } // faild updating order because: order or product don't exist in data surce
    }


    public BO.Order? ManagerUpdateOrder(int oID, int pID,int UpdatedAmount)
    {
        try
        {
            BO.Order? orderToUpdate = GetByID(oID);
            if (orderToUpdate?.ShipDate != null) // if order has been shipped then throw exception status exception
                throw new BO.ConflictingStatusException("Order has already been shipped");

            if (orderToUpdate?.Items?.Count == 0) // if there are no order items in order then throw not existing exception
                throw new DO.NotExistingException();

            //Console.WriteLine("Enter ID of product that you want to update:");
            //if (!int.TryParse(Console.ReadLine(), out int pID)) // converts the input to integer
            //    throw new BO.FailedUpdatingObjectException(new BO.IlegalDataException("Ilegal ID"));


            BO.OrderItem? orderItemToUpdate = orderToUpdate?.Items?.Find(oi => oi.ProductID == pID);
            if (orderItemToUpdate != null)
            {
                DO.Product? p = Dal.Product.GetIf(item => (item?.ID ?? 0) == pID);
                int pAmount = p?.InStock ?? 0 + orderItemToUpdate.Amount; // max amount in stock
                //Console.WriteLine($"Enter products updated amount, up to-{pAmount}");
                //int UpdatedAmount;
                if (/*!int.TryParse(Console.ReadLine(), out UpdatedAmount) ||*/ UpdatedAmount < 0 || UpdatedAmount > pAmount) // if input is ilegal
                    throw new BO.IlegalDataException("Invalid amount");

                orderToUpdate?.Items?.Remove(orderItemToUpdate); // remove item from order
                if (UpdatedAmount == 0) // if amount to update is zero  
                    Dal.OrderItem.Delete(orderItemToUpdate.ID); // deleting item from order list (in data surce) 
                else
                {
                    Dal.OrderItem.Update(new DO.OrderItem // updating list of Order item (in data surce)
                    {
                        ID = orderItemToUpdate.ID,
                        ProductId = orderItemToUpdate.ProductID,
                        OrderId = oID,
                        Price = orderItemToUpdate.Price,
                        Amount = UpdatedAmount
                    });
                    orderToUpdate?.Items?.Add(new BO.OrderItem // updating order item list of order
                    {
                        ID = orderItemToUpdate.ID,
                        ProductID = orderItemToUpdate.ProductID,
                        Name = orderItemToUpdate.Name,
                        Amount = UpdatedAmount,
                        Price = orderItemToUpdate.Price,
                        TotalPrice = UpdatedAmount * orderItemToUpdate.Price
                    });
                }
                Dal.Product.Update(new DO.Product()  // updating product amount in product list 
                {
                    ID = p?.ID ?? 0,
                    Name = p?.Name,
                    Category = p?.Category,
                    Price = p?.Price ?? 0,
                    InStock = p?.InStock ?? 0 + orderItemToUpdate.Amount - UpdatedAmount // updating stock of products
                });
            }
            return orderToUpdate;

        }
        catch (Exception ex) { throw new BO.FailedUpdatingObjectException(ex); } // faild updating order because: order or product don't exist in data surce or ilegal ID  
    }

    /// <summary>
    ///  the method receives as input an order and returns its status
    /// </summary>
    /// <param name="o"></param>
    /// <returns>string</returns>
    public string OrderStatus(DO.Order? o)
    {
        string oStatus; // status of current order
        if (o?.DeliveryDate != null) // checking if order has been deliverd
            oStatus = "OrderDelivered";
        else if (o?.ShipDate != null) // checking if order has been shipped
            oStatus = "OrderShipped";
        else oStatus = "OrderConfirmed";
        return oStatus;
    }

}

