

using DO;
using static Dal.DataSource;
using static Dal.DataSource.Config;
using DalApi;
using System.Security.Cryptography;


namespace Dal;

internal class DalOrderItem : IOrderItem
{
    /// <summary>
    /// Adding a new order item to list. If order item (to add) allready exists then throw error.
    /// </summary>
    /// <returns>int?</returns>
    /// <param name="oi"></param>
    /// <exception cref="Exception"></exception>
    public int Add(OrderItem oi)
    {
        if (orderItemList.Contains(oi))
            throw new AlreadyExistingException();
        oi.ID = getIdNewOI();
        orderItemList.Add(oi);
        return (int)oi.ID;
    }
    /// <summary>
    /// Deleteing order item from list. If order item (to delete) does not exists then throw error.
    /// </summary>
    /// <param name="oi"></param>
    /// <exception cref="Exception"></exception>
    public void Delete(int oiID)
    {
        OrderItem? delOI = orderItemList.Find(OI => OI?.ID == oiID);
        if (delOI == null)
            throw new NotExistingException();
        orderItemList.Remove(delOI);
    }
    /// <summary>
    /// Updating an order item in list. If order item (to update) does not exist then throw error.
    /// </summary>
    /// <param name="oi"></param>
    public void Update(OrderItem oi)
    {
        var OrderItemToUpdate = orderItemList.FirstOrDefault(item => item?.ID == oi.ID);
        if (OrderItemToUpdate != null)
        {
            orderItemList.Remove(OrderItemToUpdate);
            orderItemList.Add(oi);
        }
        else throw new NotExistingException();
    }

    /// <summary>
    /// return list of all OrderItems
    /// </summary>
    /// <returns>IEnumerable</returns>
    public IEnumerable<OrderItem?> GetList(Func<OrderItem?, bool>? condition) => orderItemList.Where(orderItem => condition is null ? true : condition(orderItem));


    /// <summary>
    /// returns the first OrderItem in orderList that fulfils the condition
    /// </summary>
    /// <param name="condition"></param>
    /// <returns>OrderItem?</returns>
    public OrderItem? GetIf(Func<OrderItem?, bool> condition) => orderItemList.FirstOrDefault(condition);


    /// <summary>
    /// returning a grouped list of order items according to orders id
    /// </summary>
    /// <returns>IEnumerable</returns>
    public IEnumerable<IGrouping<int, OrderItem?>> GetGrouped()
    {
        var GroupedLst = from orderItem in orderItemList
                         group orderItem by (int)orderItem?.OrderId! into orderGroup
                         select orderGroup;
        return GroupedLst;
    }
}
