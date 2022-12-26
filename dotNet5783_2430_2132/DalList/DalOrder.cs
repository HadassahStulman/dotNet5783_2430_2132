

using DO;
using static Dal.DataSource;
using static Dal.DataSource.Config;
using DalApi;
using System.Security.Cryptography;

namespace Dal;

internal class DalOrder : IOrder
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
    public void Delete(int oID)
    {
        Order? delOrder = orderList.Find(order => order?.ID == oID);
        if (delOrder == null)
            throw new NotExistingException();
        orderList.Remove(delOrder);
        //bool flag = false;
        //for (int i = 0; i < orderList.Count; i++)
        //    if (orderList[i] != null && orderList[i]?.ID == oID)
        //    {
        //        orderList.RemoveAt(i);
        //        flag = true;
        //    }
        //if (!flag)
        //    throw new NotExistingException();
    }
    /// <summary>
    /// Updating an order item in list. If order item (to update) does not exist then throw error.
    /// </summary>
    /// <param name="o"></param>
    public void Update(Order o)
    {
        var orderToUpdate = orderList.FirstOrDefault(order => (order?.ID ?? 0) == o.ID);
        if (orderToUpdate != null)
        {
            orderList.Remove(orderToUpdate);
            orderList.Add(o);
        }
        else throw new NotExistingException();
    }

    /// <summary>
    /// return list of all Orders
    /// </summary>
    /// <returns>IEnumerable<Order></Order></returns>
    public IEnumerable<Order?> GetList(Func<Order?, bool>? condition)=> orderList.Where(order => condition is null ? true : condition(order));

    /// <summary>
    /// returns the first Order in orderList that fulfils the condition
    /// </summary>
    /// <param name="condition"></param>
    /// <returns>Order?</returns>
    public Order? GetIf(Func<Order?, bool> condition) => orderList.FirstOrDefault(condition);
}
