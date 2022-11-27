

using DO;
using static Dal.DataSource;
using static Dal.DataSource.Config;
using System.Collections.Generic;
using System.Collections;
using DalApi;
namespace Dal;

internal class DalOrder: IOrder
{

    /// <summary>
    /// Adding a new order to list. If order (to add) allready exists then throw error.
    /// </summary>
    /// <returns>int</returns>
    /// <param name="o"></param>
    /// <exception cref="Exception"></exception>
    public int? Add(Order o)
    {
        if (orderList.Contains(o))
            throw new AlreadyExistingException();
        o.ID = getIdNewO();
        orderList.Add(o);
        return o.ID;
    }
    /// <summary>
    /// Deleteing order from list. If order (to delete) does not exists then throw error.
    /// </summary>
    /// <param name="o"></param>
    /// <exception cref="Exception"></exception>
    public void Delete(int? oID)
    {
        bool flag = false;
        for (int i = 0; i < orderList.Count; i++)
            if (orderList[i].ID == oID)
            {
                orderList.RemoveAt(i);
                flag = true;
            }
        if (!flag)
            throw new NotExistingException();
    }
    /// <summary>
    /// Updating an order item in list. If order item (to update) does not exist then throw error.
    /// </summary>
    /// <param name="o"></param>
    public void Update(Order o)
    {
        bool flag = false;
        for (int i = 0; i < orderList.Count(); i++)
        {
            if (orderList[i].ID == o.ID)
            {
                orderList[i] = o;
                flag = true;
                break;
            }
        }
        if (!flag)
            throw new NotExistingException();
    }
    /// <summary>
    /// recieves Uniqe ID for identifing the order and returns Order object
    /// </summary>
    /// <param name="o"></param>
    /// <returns>Order</returns>
    public Order GetByID(int? oId)
    {
        bool flag = false;
        int i = 0;
        for (; i < orderList.Count(); i++)
            if (orderList[i].ID == oId)
            {
                flag = true;
                break;
            }
        if (!flag)
            throw new NotExistingException();
        return orderList[i];
    }

    /// <summary>
    /// return list of all Product
    /// </summary>
    /// <returns>IEnumerable<Order></Order></returns>
    public IEnumerable<Order> GetList()
    {
        List<Order> orders = new List<Order>();
        orders.AddRange(orderList);
        return orders;
    }
}
