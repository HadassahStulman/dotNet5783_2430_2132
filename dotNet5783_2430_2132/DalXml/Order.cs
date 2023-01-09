

using DalApi;

namespace Dal;

internal class Order : IOrder
{
    public int Add(DO.Order entity)
    {
        throw new NotImplementedException();
    }

    public void Delete(int id)
    {
        throw new NotImplementedException();
    }

    public DO.Order? GetIf(Func<DO.Order?, bool> func)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<DO.Order?> GetList(Func<DO.Order?, bool>? conditon = null)
    {
        throw new NotImplementedException();
    }

    public void Update(DO.Order entity)
    {
        throw new NotImplementedException();
    }
}
