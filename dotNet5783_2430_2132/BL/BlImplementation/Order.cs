
using BlApi;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace BlImplementation;

internal class Order : IOrder
{
    /// <summary>
    /// private field for allowing accsess from BL to Dal
    /// </summary>
    private DalApi.IDal Dal = DalApi.Factory.Get()!;

    /// <summary>
    /// returns list of all orders for display
    /// FOR MANAGER
    /// </summary>
    /// <returns>IEnumerable<BO.OrderForList></OrderForList></returns>
    public IEnumerable<BO.OrderForList?> GetList(Func<BO.OrderForList?, bool>? condition)
    {
        try
        {
            var ofllst =
                from order in Dal.Order.GetList()
                where order != null
                let oStatus = OrderStatus(order) // status of current order
                let oiLst = Dal.OrderItem.GetGrouped().FirstOrDefault(oiGroup => oiGroup.Key == order?.ID)
                let oAmount = oiLst.Count()
                let oTotalPrice = oiLst.Sum(oi => (oi?.Price ?? 0) * (oi?.Amount ?? 0))
                orderby order?.OrderDate
                let ofl = new BO.OrderForList
                {
                    ID = order?.ID ?? 0,
                    CustomerName = order?.CustomerName,
                    Status = (BO.Enums.OrderStatus)Enum.Parse(typeof(BO.Enums.OrderStatus), oStatus),
                    AmountOfItems = oAmount,
                    TotalPrice = oTotalPrice
                } // adding order to list of- OrderForList
                where condition is null ? true : condition(ofl)
                select ofl;
            return ofllst;
        }
        catch (Exception ex) { throw new BO.FailedGettingObjectException(ex); }; // if order has 0 items then don't add it to OrderForList
    }

    public BO.Order GetByID(int oID)
    {
        try
        {
            if (oID < 100000) // if order ID is ilegal then throw iligal data exception
                throw new BO.IlegalDataException("Ilegal order ID");

            var dalGroupedList = Dal.OrderItem.GetGrouped();
            IGrouping<int, DO.OrderItem?>? orderItemsInOrder = dalGroupedList.FirstOrDefault(item => item.Key == oID);
            var BOoiLst =
                from orderItem in orderItemsInOrder ?? throw new BO.IlegalDataException("Order does not contain any items")
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

            int oAmount = orderItemsInOrder.Sum(ordetItem => ordetItem?.Amount ?? 0);
            double oTotalPrice = orderItemsInOrder.Sum(ordetItem => (ordetItem?.Price ?? 0) * (ordetItem?.Amount ?? 0));
            DO.Order? order = Dal.Order.GetIf(item => (item?.ID ?? 0) == oID);
            BO.Order orderToGet = DO.ExtentionMethods.ConvertTo(order, new BO.Order())!;
            orderToGet.Status = (BO.Enums.OrderStatus)Enum.Parse(typeof(BO.Enums.OrderStatus), OrderStatus(order)); //convert string to enum   
            orderToGet.Items = new ObservableCollection<BO.OrderItem>(BOoiLst);
            orderToGet.TotalPrice = oTotalPrice;
            return orderToGet;
        }
        catch (Exception ex) { throw new BO.FailedGettingObjectException(ex); } // if order or product dos not exist in data surce the throw not existing exception
    }


    public BO.Order UpdateShipping(int oID)
    {
        try
        {
            if (oID < 10000) // if order ID is a negative number then throw iligal data exception
                throw new BO.IlegalDataException("Ilegal order ID");
            DO.Order? dOrder = Dal.Order.GetIf(item => item?.ID == oID);
            if (dOrder?.ShipDate != null) // checking if the order was already shipped. if so then throw status exception
                throw new BO.ConflictingStatusException("The order has already been shipped");
            DO.Order orderToUpdate = DO.ExtentionMethods.ConvertTo(dOrder, new DO.Order())!;
            orderToUpdate.ShipDate = DateTime.Now;
            orderToUpdate.DeliveryDate = null;
            Dal.Order.Update(orderToUpdate);
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

            DO.Order orderToUpdate = DO.ExtentionMethods.ConvertTo(dOrder, new DO.Order())!;
            orderToUpdate.DeliveryDate = DateTime.Now;
            Dal.Order.Update(orderToUpdate);
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


    public BO.Order? ManagerUpdateOrder(int oID, int pID, int UpdatedAmount)
    {
        try
        {
            BO.Order? orderToUpdate = GetByID(oID);
            if (orderToUpdate?.ShipDate != null) // if order has been shipped then throw exception status exception
                throw new BO.ConflictingStatusException("Order has already been shipped");

            if (orderToUpdate?.Items?.Count == 0) // if there are no order items in order then throw not existing exception
                throw new BO.IlegalDataException("Cart Is Empty");


            BO.OrderItem? orderItemToUpdate = orderToUpdate?.Items?.ToList().Find(oi => oi.ProductID == pID);
            if (orderItemToUpdate != null)
            {
                DO.Product? p = Dal.Product.GetIf(item => (item?.ID ?? 0) == pID);
                int pAmount = p?.InStock ?? 0 + orderItemToUpdate.Amount; // max amount in stock
                if (UpdatedAmount < 0) // if input is ilegal
                    throw new BO.IlegalDataException("Invalid amount");
                if (UpdatedAmount > pAmount)
                    throw new BO.OutOfStockException();
                orderToUpdate?.Items?.Remove(orderItemToUpdate); // remove item from order
                orderToUpdate!.TotalPrice += (UpdatedAmount - orderItemToUpdate.Amount) * orderItemToUpdate.Price;
                if (UpdatedAmount == 0) // if amount to update is zero  
                    Dal.OrderItem.Delete(orderItemToUpdate.ID); // deleting item from order list (in data surce) 
                else
                {
                    // updating order item data source in DO
                    DO.OrderItem DupdatedOI = DO.ExtentionMethods.ConvertTo(orderItemToUpdate, new DO.OrderItem());
                    DupdatedOI.Amount = UpdatedAmount;
                    Dal.OrderItem.Update(DupdatedOI);
                    
                    // updating order item list in order
                    BO.OrderItem BupdatedOI = DO.ExtentionMethods.ConvertTo(orderItemToUpdate, new BO.OrderItem())!;
                    BupdatedOI.Amount = UpdatedAmount;
                    BupdatedOI.TotalPrice = UpdatedAmount * orderItemToUpdate.Price;
                    orderToUpdate.Items?.Add(BupdatedOI);
                }

                // updating product data source in DO
                DO.Product DUpdatedProduct = DO.ExtentionMethods.ConvertTo(p, new DO.Product());
                DUpdatedProduct.InStock = p?.InStock ?? 0 + orderItemToUpdate.Amount - UpdatedAmount; // updating stock of products
                Dal.Product.Update(DUpdatedProduct);
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

    public BO.Order? NextOrderToManage()
    {
        try
        {
            // create list of orders ordered by the latest update date
            var lst = from order in Dal.Order.GetList()
                      orderby order?.ShipDate != null ? order?.ShipDate : order?.OrderDate
                      where order?.DeliveryDate == null
                      select order;
            DO.Order? DOorderToManage = lst.FirstOrDefault(); // oldest
            return GetByID(DOorderToManage?.ID ?? 0);
        }
        catch (Exception)
        {
            return null;
        }
    }
}

