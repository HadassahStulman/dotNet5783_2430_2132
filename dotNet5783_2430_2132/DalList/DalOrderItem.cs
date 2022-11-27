

using DO;
using static Dal.DataSource;
using static Dal.DataSource.Config;
using System.Collections.Generic;
using System.Collections;
using DalApi;
namespace Dal;

internal class DalOrderItem:IOrderItem
{
    /// <summary>
    /// Adding a new order item to list. If order item (to add) allready exists then throw error.
    /// </summary>
    /// <returns>int?</returns>
    /// <param name="oi"></param>
    /// <exception cref="Exception"></exception>
    public int? Add(OrderItem oi)
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
    public void Delete(int? oiID)
    {
        bool flag = false;
        for (int i = 0; i < orderItemList.Count; i++)
            if (orderItemList[i].ID == oiID)
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
        bool flag = false;
        for (int i = 0; i < orderItemList.Count(); i++)
            if (orderItemList[i].ID == oi.ID)
            {
                orderItemList[i] = oi;
                flag = true;
                break;
            }
        if (!flag)
            throw new NotExistingException();
    }
    /// <summary>
    /// recieves Uniqe ID for identifing the order item and returns order item object
    /// </summary>
    /// <param name="oi"></param>
    /// <returns>OrderItem</returns>
    public OrderItem GetByID(int? oiId)
    {
        bool flag = false;
        int i = 0;
        for (; i < orderItemList.Count(); i++)
            if (orderItemList[i].ID == oiId)
            {
                flag = true;
                break;
            }
        if (!flag)
            throw new NotExistingException();
        return orderItemList[i];
    }

    /// <summary>
    /// return list of all Product
    /// </summary>
    /// <returns>IEnumerable</returns>
    public IEnumerable<OrderItem> GetList()
    {
        List<OrderItem> orderIs = new List<OrderItem>();
        orderIs.AddRange(orderItemList);
        return orderIs;
    }

    public OrderItem GetByBothID(int? pID, int? oID)
    {
        bool flag = false;
        OrderItem oi = new OrderItem();
        foreach(OrderItem item in orderItemList)
        {
            if(item.ProductId == pID && item.OrderId== oID)
            {
                oi = item;
                flag = true;
                break;
            }
        }
        if (!flag)
            throw new NotExistingException();
        return oi;
        
    }
    /// <summary>
    /// return list of all order items in a specific order
    /// </summary>
    /// <param name="oID"></param>
    /// <returns>IEnumerable<OrderItem></returns>
    /// <exception cref="NotExistingException"></exception>
    public IEnumerable<OrderItem> GetAllItemsInOrder(int? oID)
    {
        List<OrderItem> lst = new List<OrderItem>();
        foreach(OrderItem item in orderItemList) // runs across order item list 
            if(item.OrderId == oID) // if item is in the order
                lst.Add(item);
        if (lst.Count == 0) 
            throw new NotExistingException();
        return lst;
    }
}
