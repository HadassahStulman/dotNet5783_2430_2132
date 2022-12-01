
namespace BO;
/// <summary>
/// class for managing order list screen
/// </summary>
public class OrderForList
{
    /// <summary>
    ///  Order ID 
    /// </summary>
    public int ID { get; set; }
    /// <summary>
    /// name of customer 
    /// </summary>
    public string? CustomerName { get; set; }   
    /// <summary>
    /// status of order
    /// </summary>
    public Enums.OrderStatus? Status { get; set; }
    /// <summary>
    /// amount of items items in order
    /// </summary>
    public int AmountOfItems { get; set; } 
    /// <summary>
    /// total order price
    /// </summary>
    public double TotalPrice { get; set; }

    public override string ToString() => $@"
    Order ID={ID}
    customer's name: {CustomerName}
    order's status: {Status}
    amount of items: {AmountOfItems}
    total price: {TotalPrice}";

}
