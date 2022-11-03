

using DO;
using static Dal.DataSource;
using System.Collections.Generic;
using System.Collections;
namespace Dal;

public class DalOrderItem
{
    /// <summary>
    /// Adding a new order item to list. If order item (to add) allready exists then throw error.
    /// </summary>
    /// <param name="oi"></param>
    /// <exception cref="Exception"></exception>
    public void Add(OrderItem oi)
    {
        if (orderItemList.Contains(oi))
            throw new Exception("Order item already exists\n");
        orderItemList.Add(oi);
    }
    /// <summary>
    /// Deleteing order item from list. If order item (to delete) dos not exists then throw error.
    /// </summary>
    /// <param name="oi"></param>
    /// <exception cref="Exception"></exception>
    public void Delete(OrderItem oi)
    {
        if (!orderItemList.Contains(oi))
            throw new Exception("Order item does not exist\n");
        orderItemList.Remove(oi);
    }
    /// <summary>
    /// Updateing an order item in list. If order item (to update) dos not exist then throw error.
    /// </summary>
    /// <param name="oi"></param>
    public void Update(OrderItem oi)
    {
        for (int i = 0; i < orderItemList.Count(); i++)
            if (orderItemList[i].OrderId == oi.OrderId && orderItemList[i].ProductId == oi.ProductId)
            {
                orderItemList[i] = oi;
                break;
            }
        throw new Exception("Order item dos not exist\n");
    }
    /// <summary>
    /// returns Uniqe ID for identifing the order (that contains this item)
    /// </summary>
    /// <param name="oi"></param>
    /// <returns>int</returns>
    public int GetID(OrderItem oi) { return oi.OrderId; }
    public IEnumerable getList()
    {
        return orderItemList;
    }
}
