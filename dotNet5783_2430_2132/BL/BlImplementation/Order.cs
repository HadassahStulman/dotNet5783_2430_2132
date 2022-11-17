
using BlApi;
using BO;

namespace BlImplementation;

internal class Order : IOrder
{
    /// <summary>
    /// private field for allowing accsess from BL to Dal
    /// </summary>
    private DalApi.IDal Dal = new Dal.DalList();
    public BO.Order GetByID(int oID)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<OrderForList> GetList()
    {
        throw new NotImplementedException();
    }

    public BO.Order ManagerUpdateOrder(int oID)
    {
        throw new NotImplementedException();
    }

    public OrderTracking TrackOrder(int oID)
    {
        throw new NotImplementedException();
    }

    public BO.Order UpdateDelivery(int oID)
    {
        throw new NotImplementedException();
    }

    public BO.Order UpdateShipping(int oID)
    {
        throw new NotImplementedException();
    }
}
