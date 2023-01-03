using DO;

namespace DalApi;

/// <summary>
/// interface for Order item behavior
/// </summary>
public interface IOrderItem:ICrud<OrderItem>
{
    /// <summary>
    /// return list of all OrderItems Grouped by Orders
    /// </summary>
    /// <returns>IEnumerable<IGrouping<int, OrderItem?>></returns>
    public IEnumerable<IGrouping<int, OrderItem?>> GetGrouped();
}
