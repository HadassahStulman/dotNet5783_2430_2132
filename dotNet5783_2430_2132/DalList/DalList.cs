
using DalApi;

namespace Dal;
internal sealed class DalList : IDal
{
    /// <summary>
    /// create the properties that are declared in IDal and returns the object
    /// </summary>
    public IProduct Product { get; } = new DalProduct();
    public IOrder Order { get; } = new DalOrder();
    public IOrderItem OrderItem { get; } = new DalOrderItem();
    public static IDal Instance { get; } = new DalList();

    /// <summary>
    /// constructor
    /// </summary>
    private DalList() { }
}
