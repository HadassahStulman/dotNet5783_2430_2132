
using static DO.Enums;

namespace DO;
/// <summary>
/// Structure for a item in order
/// </summary>
public struct OrderItem
{
    /// <summary>
    /// Uniqe id to identify order item
    /// </summary>
    public int? ID { get; set; }
    /// <summary>
    /// Uniqe ID for identifing a item (in this order)
    /// </summary>
    public int? ProductId { get; set; }
    /// <summary>
    /// Uniqe ID for identifing the order (that contains this item)
    /// </summary>
    public int? OrderId { get; set; }
    /// <summary>
    /// price for item
    /// </summary>
    public double? Price { get; set; }
    /// <summary>
    /// amount of this item in order
    /// </summary>
    public int? Amount { get; set; }
    /// <summary>
    /// prints decription of an item in order
    /// </summary>
    /// <returns>string</returns>
    public override string ToString() => $@"
        Order Item ID: {ID}
        Product ID: {ProductId}
        Order ID: {OrderId}
    	Price: {Price}
    	Amount of copies: {Amount}
";
}
