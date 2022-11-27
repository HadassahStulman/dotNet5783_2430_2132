
namespace BO;

/// <summary>
/// information of a single item in order or cart
/// </summary>
public class OrderItem
{
    /// <summary>
    /// item's ID
    /// </summary>
    public int? ID { get; set; }
    /// <summary> 
    /// product's name 
    /// </summary>
    public string? Name { get; set; }
    /// <summary>
    /// product's ID 
    /// </summary>
    public int? ProductID { get; set; }
    /// <summary>
    /// price of a single product
    /// </summary>
    public double? Price { get; set; }
    /// <summary>
    /// amount of copies of the product
    /// </summary>
    public int? Amount { get; set; }
    /// <summary>
    /// total price of item (price of all copies in total)
    /// </summary>
    public double? TotalPrice { get; set; }

    public override string ToString() => $@"
    order Idem's ID={ID}
    customer's name: {Name}
    product's ID: {ProductID}
    product's price: {Price}
    product's amount: {Amount}
    order items total price: {TotalPrice}";

}
