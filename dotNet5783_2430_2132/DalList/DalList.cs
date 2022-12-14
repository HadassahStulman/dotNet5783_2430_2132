
using DalApi;


namespace Dal;
internal sealed class DalList:IDal
{
    /// <summary>
    /// create the properties that are declared in IDal and returns the object
    /// </summary>
    public IProduct Product => new DalProduct();
    public IOrder Order => new DalOrder();
    public IOrderItem OrderItem => new DalOrderItem();
    public static IDal Instance { get; } = new DalList();
    
    /// <summary>
    /// constructor
    /// </summary>
    private DalList() {}
}
