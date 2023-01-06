
namespace DalApi;

public interface IDal
{ 
    /// <summary>
    /// property for each entity, with get option only
    /// </summary>
    public IProduct Product { get; }
    public IOrder Order { get; }
    public IOrderItem OrderItem { get; }
}
