

namespace BO;

/// <summary>
/// information of a single order
/// </summary>
public class Order
{
    /// <summary>
    /// order id
    /// </summary>
    public int ID { get; set; }
    /// <summary>
    /// customer's name
    /// </summary>
    public string? CustomerName { get; set; }
    /// <summary>
    /// customer's Email adress
    /// </summary>
    public string? CustomerEmail { get; set; }
    /// <summary>
    /// customer's adress for delivery
    /// </summary>
    public string? CustomerAdress { get; set; }
    /// <summary>
    /// date of ordering 
    /// </summary>
    public DateTime? OrderDate { get; set; }
    /// <summary>
    /// status of order (if shiped, delivered...)
    /// </summary>
    public Enums.OrderStatus? Status { get; set; }
    /// <summary>
    /// date of order confirmation / pament
    /// </summary>
    public DateTime? PaymentDate { get; set; }
    /// <summary>
    /// shipping date
    /// </summary>
    public DateTime? ShipDate { get; set; }
    /// <summary>
    /// delivery date
    /// </summary>
    public DateTime? DeliveryDate { get; set; }
    /// <summary>
    /// list of items in order
    /// </summary>
    public List<OrderItem>? Items { get; set; }
    /// <summary>
    /// total price of all products in order
    /// </summary>
    public double TotalPrice { get; set; }

    /// <summary>
    /// returns description of order
    /// </summary>
    /// <returns>string</returns>
    public override string ToString()
    {
        string str = $@"
Order ID={ID}
customer's name: {CustomerName}
customer's Email: {CustomerEmail}
customer's adress: {CustomerAdress}
order date: {OrderDate}
order status: {Status}
order payment date: {PaymentDate}
ship date: {ShipDate}
delivary date: {DeliveryDate}
order's total price: {TotalPrice}

list of order items:

";
        if (Items != null)
            str += string.Join("\n", Items);
        return str;
    }
}
