

using DO;
using static Dal.DataSource;
using static Dal.DataSource.Config;
using System.Collections.Generic;
using System.Collections;
namespace Dal;

public class DalOrder
{

    /// <summary>
    /// Adding a new order to list. If order (to add) allready exists then throw error.
    /// </summary>
    /// <returns>int</returns>
    /// <param name="o"></param>
    /// <exception cref="Exception"></exception>
    public int Add(Order o)
    {
        if (orderList.Contains(o))
            throw new Exception("Order already exists\n");
        o.ID = getIdNewO();
        orderList.Add(o);
        return o.ID;
    }
    /// <summary>
    /// Deleteing order from list. If order (to delete) does not exists then throw error.
    /// </summary>
    /// <param name="o"></param>
    /// <exception cref="Exception"></exception>
    public void Delete(int oID)
    {
        bool flag = false;
        for (int i = 0; i < orderList.Count; i++)
            if (orderList[i].ID == oID)
            {
                orderList.RemoveAt(i);
                flag = true;
            }
        if (!flag)
            throw new Exception("Order does not exist\n");
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
            throw new Exception("Order item does not exist\n");
    }
    /// <summary>
    /// recieves Uniqe ID for identifing the order and returns Order object
    /// </summary>
    /// <param name="o"></param>
    /// <returns>Order</returns>
    public Order GetByID(int oId)
    {
        int i = 0;
        for (; i < orderList.Count(); i++)
            if (orderList[i].ID == oId)
                break;
        return orderList[i];
    }

    /// <summary>
    /// return list of all products
    /// </summary>
    /// <returns>IEnumerable</returns>
    public IEnumerable<Order> GetList()
    {
        List<Order> orders = new List<Order>();
        orders.AddRange(orderList);
        return orders;
    }
    /// <summary>
    /// prints a description of all orders in list.
    /// </summary>
    public void PrintAllOrders()
    {
        for (int i = 0; i < orderList.Count; i++)
            Console.WriteLine(orderItemList[i]); // prints description of current order.
    }
}
