

using DalApi;

namespace Dal;

sealed internal class DalXml:IDal
{
    public static IDal Instance { get; }= new DalXml();

	private DalXml(){ }

    /// <summary>
    /// property for each entity, with get option only
    /// </summary>
    public IProduct Product { get; } = new Dal.Product();
    public IOrder Order { get; }=new Dal.Order();
    public IOrderItem OrderItem { get; } = new Dal.OrderItem();
}
