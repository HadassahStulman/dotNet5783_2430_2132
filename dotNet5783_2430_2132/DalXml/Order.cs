
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
        List<DO.Order?> orderList = (XMLTools.LoadListFromXML<DO.Order>(FPath) ?? new List<DO.Order>()).Cast<DO.Order?>().ToList();
        orderToAdd.ID = XMLTools.getIdNewO();
        DO.Order? order = orderList.FirstOrDefault(order => order?.ID == orderToAdd.ID);
        if (order != null)
            throw new DO.AlreadyExistingException();
        orderList?.Add(orderToAdd);
        XMLTools.SaveListToXML(orderList!, FPath);
        return orderToAdd.ID;
    }


    /// <summary>
    /// delete order from xml file
    /// </summary>
    /// <param name="id"></param>
    /// <exception cref="DO.NotExistingException"></exception>
    public void Delete(int id)
    {
        List<DO.Order?> orderList = (XMLTools.LoadListFromXML<DO.Order>(FPath) ?? new List<DO.Order>()).Cast<DO.Order?>().ToList();
        DO.Order? orderToDelete = orderList.FirstOrDefault(Order => Order?.ID == id);
        if (orderToDelete == null) throw new DO.NotExistingException();
        orderList.Remove((DO.Order)orderToDelete);
        XMLTools.SaveListToXML(orderList, FPath);
    }

    /// <summary>
    /// update order in xml file
    /// </summary>
    /// <param name="orderToUpdate"></param>
    /// <exception cref="DO.NotExistingException"></exception>
    public void Update(DO.Order orderToUpdate)
    {
        List<DO.Order?> orderList = (XMLTools.LoadListFromXML<DO.Order>(FPath) ?? new List<DO.Order>()).Cast<DO.Order?>().ToList();
        if (orderList.FirstOrDefault(Order => Order?.ID == orderToUpdate.ID) == null)
            throw new DO.NotExistingException();
        orderList.RemoveAll(order => order?.ID == orderToUpdate.ID);
        orderList.Add(orderToUpdate);
        XMLTools.SaveListToXML(orderList, FPath);
    }

    /// <summary>
    /// get order from xml if it fulfills a condition
    /// </summary>
    /// <param name="func"></param>
    /// <returns>DO.Order?</returns>
    /// <exception cref="DO.NotExistingException"></exception>
    public DO.Order? GetIf(Func<DO.Order?, bool> func)
    {
        IEnumerable<DO.Order?>? orderList = (XMLTools.LoadListFromXML<DO.Order>(FPath) ?? new List<DO.Order>()).Cast<DO.Order?>();
        DO.Order? order = orderList.FirstOrDefault(order => func(order));
        if (order == null) throw new DO.NotExistingException();
        return order;
    }


    /// <summary>
    /// get list of all orders that fulfills the condition
    /// </summary>
    /// <param name="condition"></param>
    /// <returns>IEnumerable<DO.Order?></returns>
    public IEnumerable<DO.Order?> GetList(Func<DO.Order?, bool>? condition = null)
    {
        List<DO.Order> orderListXML = XMLTools.LoadListFromXML<DO.Order>(FPath) ?? new List<DO.Order>();
        var Olst = orderListXML.Where(order => condition == null ? true : condition(order));
        return Olst.Cast<DO.Order?>();
    }
}
