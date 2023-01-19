using DalApi;
namespace Dal;

internal class OrderItem : IOrderItem
{
    private string FPath = @"OrderItem.xml";

    /// <summary>
    /// adding a order item to file
    /// </summary>
    /// <param name="oiToAdd"></param>
    /// <returns>int</returns>
    /// <exception cref="DO.AlreadyExistingException"></exception>
    public int Add(DO.OrderItem oiToAdd)
    {
        try
        {
            if (oiToAdd.ID!=0) throw new DO.AlreadyExistingException();
            oiToAdd.ID = XMLTools.getIdNewOI();
            List<DO.OrderItem>? oiList = XMLTools.LoadListFromXML<DO.OrderItem>(FPath);
            oiList!.Add(oiToAdd);
            XMLTools.SaveListToXML(oiList, FPath);
            return oiToAdd.ID;
        }
        catch(Exception ex) { throw ex; }

    }

    /// <summary>
    /// deleting order item from file 
    /// </summary>
    /// <param name="id"></param>
    public void Delete(int id)
    {
        try
        {
            List<DO.OrderItem>? oiList = XMLTools.LoadListFromXML<DO.OrderItem>(FPath);
            DO.OrderItem? oiToDelete = oiList!.FirstOrDefault(item => item.ID == id);
            if (oiToDelete == null)
                throw new DO.NotExistingException();
            oiList!.Remove((DO.OrderItem)oiToDelete);
            XMLTools.SaveListToXML<DO.OrderItem>(oiList!, FPath);
        }
        catch (Exception ex) { throw ex; }

    }

    /// <summary>
    /// updating an order item in file
    /// </summary>
    /// <param name="oiToUpdate"></param>
    /// <exception cref="DO.NotExistingException"></exception>
    public void Update(DO.OrderItem oiToUpdate)
    {
        try
        {
            List<DO.OrderItem>? oiList = XMLTools.LoadListFromXML<DO.OrderItem>(FPath);
            DO.OrderItem? oi = oiList!.FirstOrDefault(item => item.ID == oiToUpdate.ID);
            if (oi == null)
                throw new DO.NotExistingException();
            oiList!.Remove((DO.OrderItem)oi);
            oiList!.Add(oiToUpdate);
            XMLTools.SaveListToXML<DO.OrderItem>(oiList!, FPath);
        }
        catch(Exception ex) { throw ex; }
    }

    /// <summary>
    /// returs a order item that fulfills the condition of func
    /// </summary>
    /// <param name="func"></param>
    /// <returns>DO.OrderItem?</returns>
    /// <exception cref="DO.NotExistingException"></exception>
    public DO.OrderItem? GetIf(Func<DO.OrderItem?, bool> func)
    {
        return (GetList(func) ?? throw new DO.NotExistingException()).First();
    }

    /// <summary>
    /// return list of all OrderItems that fulfill the condition
    /// </summary>
    /// <returns>IEnumerable</returns>
    public IEnumerable<DO.OrderItem?> GetList(Func<DO.OrderItem?, bool>? condition = null)
    {
        try
        {
            List<DO.OrderItem>? oiList = XMLTools.LoadListFromXML<DO.OrderItem>(FPath);
            var newOiList = from oi in oiList
                            where condition == null ? true : condition(oi)
                            select oi;
            return newOiList.Cast<DO.OrderItem?>();
        }
        catch (Exception ex) { throw ex; }
    }

    /// <summary>
    /// returning a grouped list of order items according to orders id
    /// </summary>
    /// <returns>IEnumerable</returns>
    public IEnumerable<IGrouping<int, DO.OrderItem?>> GetGrouped()
    {
        try
        {
            IEnumerable<DO.OrderItem?> orderItemList = (XMLTools.LoadListFromXML<DO.OrderItem>(FPath) ?? new List<DO.OrderItem>()).AsEnumerable().Cast<DO.OrderItem?>();
            var GroupedLst = from oi in orderItemList
                             group oi by (int)oi?.OrderId! into orderGroup
                             select orderGroup;
            return GroupedLst;
        }
        catch (Exception ex) { throw ex; }
    }

}
