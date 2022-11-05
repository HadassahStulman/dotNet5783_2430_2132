

using DO;
using static Dal.DataSource;
using System.Collections.Generic;
using System.Collections;
namespace Dal;

public class DalOrder
{
    /// <summary>
    /// Adding a new order to list. If order (to add) allready exists then throw error.
    /// </summary>
    /// <param name="o"></param>
    /// <exception cref="Exception"></exception>
    public void Add(Order o)
    {
        if (orderList.Contains(o))
            throw new Exception("Order already exists\n");
        orderList.Add(o);
    }
    /// <summary>
    /// Deleteing order from list. If order (to delete) does not exists then throw error.
    /// </summary>
    /// <param name="o"></param>
    /// <exception cref="Exception"></exception>
    public void Delete(Order o)
    {
        if (!orderList.Contains(o))
            throw new Exception("Order does not exist\n");
        orderList.Remove(o);
    }
    /// <summary>
    /// Updateing an order item in list. If order item (to update) does not exist then throw error.
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
        if(!flag)
        throw new Exception("Order item does not exist\n");
    }
    /// <summary>
    /// returns Uniqe ID for identifing the order
    /// </summary>
    /// <param name="o"></param>
    /// <returns>int</returns>
    public int GetID(Order o) { return o.ID; }
    public IEnumerable getList()
    {
        return orderList;
    }
}
