

namespace BlApi;

/// <summary>
/// interface for order, contains several methods for managing orders
/// </summary>
public interface IOrder
{
    /// <summary>
    /// returns list of all orders for display
    /// FOR MANAGER
    /// </summary>
    /// <returns>IEnumerable<BO.OrderForList></OrderForList></returns>
    public IEnumerable<BO.OrderForList?> GetList(Func<BO.OrderForList?, bool>? condition=null);


    /// <summary>
    /// returns order object according to it's ID
    /// FOR MANAGER AND CUSTOMER
    /// </summary>
    /// <param name="oID"></param>
    /// <returns>BO.Order</returns>
    public BO.Order GetByID(int oID);


    /// <summary>
    /// update shipping date for a certain order, and changing order status
    /// FOR MANAGER
    /// </summary>
    /// <param name="oID"></param>
    /// <returns>BO.Order</returns>
    public BO.Order UpdateShipping(int oID);


    /// <summary>
    /// update delivery date for a certain order, and changing order status
    /// FOR MANAGER
    /// </summary>
    /// <param name="oID"></param>
    /// <returns>BO.Order</returns>
    public BO.Order UpdateDelivery(int oID);


    /// <summary>
    /// track order and returns tracking object that describes the order's tracking status 
    /// FOR MANAGER
    /// </summary>
    /// <param name="oID"></param>
    /// <returns>BO.OrderTracking</returns>
    public BO.OrderTracking TrackOrder(int oID);


    /// <summary>
    /// function aloows manager to update or delete amount of copies of product in a specific order
    /// FOR MANAGER
    /// </summary>
    /// <param name="oID"></param>
    /// <returns>BO.Order</returns>
    public BO.Order? ManagerUpdateOrder(int oID);

    /// <summary>
    ///  the method receives as input an order and returns its status
    /// </summary>
    /// <param name="o"></param>
    /// <returns>string</returns>
    public string OrderStatus(DO.Order? o);
}
