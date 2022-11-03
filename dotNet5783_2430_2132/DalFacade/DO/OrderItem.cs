

using static DO.Enums;
using System.Xml.Linq;

namespace DO;

public struct OrderItem
{
    /// <summary>
    /// Uniqe ID for identifing a item (in this order)
    /// </summary>
    public int ProductId { get; set; }
    /// <summary>
    /// Uniqe ID for identifing the order (that contains this item)
    /// </summary>
    public int OrderId { get; set; }
    /// <summary>
    /// price for item
    /// </summary>
    public double Price { get; set; }
    /// <summary>
    /// amount of this item in order
    /// </summary>
    public int Amount { get; set; }
    /// <summary>
    /// prints decription of a item in order
    /// </summary>
    /// <returns>string</returns>
    public override string ToString() => $@"
        Product ID: {ProductId}
        Order ID: {OrderId}
    	Price: {Price}
    	Amount of product: {Amount}
";
}
