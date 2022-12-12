

using DO;
using static Dal.DataSource;
using static Dal.DataSource.Config;
using DalApi;


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
        bool flag = false;
        for (int i = 0; i < orderItemList.Count; i++)
            if (orderItemList[i]?.ID == oiID)
            {
                orderItemList.RemoveAt(i);
                flag = true;
            }
        if (!flag)
            throw new NotExistingException();
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
}
