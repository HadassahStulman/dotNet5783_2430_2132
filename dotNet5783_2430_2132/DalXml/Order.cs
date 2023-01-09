
using DalApi;
using System.Data;
using static Dal.DataSource.Config;

namespace Dal;

internal class Order : IOrder
{
    private readonly string FPath = @"Order.xml";

    /// <summary>
    /// get data from xml file and adds new order
    /// </summary>
    /// <param name="orderToAdd"></param>
    /// <returns></returns>
    /// <exception cref="DO.AlreadyExistingException"></exception>
    public int Add(DO.Order orderToAdd)
    {
        List<DO.Order> orderList = XMLTools.LoadListFromXML<DO.Order>(FPath) ?? new List<DO.Order>();
        if (orderList.Contains(orderToAdd))
            throw new DO.AlreadyExistingException();
        orderToAdd.ID = XMLTools.getIdNewO();
        orderList.Add(orderToAdd);
        XMLTools.SaveListToXML(orderList, FPath);
        return orderToAdd.ID;
    }


    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <exception cref="DO.NotExistingException"></exception>
    public void Delete(int id)
    {
        List<DO.Order> orderList = XMLTools.LoadListFromXML<DO.Order>(FPath) ?? new List<DO.Order>();
        DO.Order? orderToDelete = orderList.FirstOrDefault(Order => Order.ID == id);
        if (orderToDelete == null)  throw new DO.NotExistingException();
        orderList.Remove((DO.Order)orderToDelete);
        XMLTools.SaveListToXML(orderList, FPath);
    }

    public void Update(DO.Order orderToUpdate)
    {
        List<DO.Order> orderList = XMLTools.LoadListFromXML<DO.Order>(FPath) ?? new List<DO.Order>();
        if (!orderList.Contains(orderToUpdate))
            throw new DO.NotExistingException();
        orderList.RemoveAll(order => order.ID == orderToUpdate.ID);
        orderList.Add(orderToUpdate);
        XMLTools.SaveListToXML(orderList, FPath);
    }

    public DO.Order? GetIf(Func<DO.Order?, bool> func)
    {
        List<DO.Order>? orderList = XMLTools.LoadListFromXML<DO.Order>(FPath) ?? new List<DO.Order>();
        DO.Order? order = orderList.FirstOrDefault(order => func(order));
        if(order==null) throw new DO.NotExistingException();
        return order;
    }

    public IEnumerable<DO.Order?> GetList(Func<DO.Order?, bool>? conditon = null)
    {
        List<DO.Order> orderListXML = XMLTools.LoadListFromXML<DO.Order>(FPath) ?? new List<DO.Order>();
        var Olst = from order in orderListXML
                   where conditon == null ? true : conditon(order)
                   select order;
        return Olst.Cast<DO.Order?>();
    }
}
