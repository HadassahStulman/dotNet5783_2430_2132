
using BO;
namespace BlApi;

public interface IOrder
{
    /// <summary>
    /// returns list of all orders
    /// </summary>
    /// <returns>IEnumerable<OrderForList></OrderForList></returns>
    public IEnumerable<OrderForList> GetList();
    /// <summary>
    /// returns order object according to it's ID
    /// </summary>
    /// <param name="oID"></param>
    /// <returns>Order</returns>
    public Order GetByID(int oID);
    /// <summary>
    /// update shipping date for a certain order, and changing order status
    /// </summary>
    /// <param name="oID"></param>
    /// <returns>Order</returns>
    public Order UpdateShipping(int oID);
    /// <summary>
    /// update delivery date for a certain order, and changing order status
    /// </summary>
    /// <param name="oID"></param>
    /// <returns>Order</returns>
    public Order UpdateDelivery(int oID);
    /// <summary>
    /// track order and returns tracking object that describes the order's tracking status 
    /// </summary>
    /// <param name="oID"></param>
    /// <returns>OrderTracking</returns>
    public OrderTracking TrackOrder(int oID);


}
