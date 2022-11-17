

namespace BO;

/// <summary>
/// information about customer's shopping cart
/// </summary>
public class Cart
{
    /// <summary>
    /// customer's name
    /// </summary>
    public string? CustomerName { get; set; }
    /// <summary>
    /// customer's Email adress
    /// </summary>
    public string? CustomerEmail { get; set; }
    /// <summary>
    /// customer's adress
    /// </summary>
    public string? CustomerAdress { get; set; }
    /// <summary>
    /// list of all items in cart
    /// </summary>
    public List<OrderItem>? Items { get; set; }
    /// <summary>
    /// total price of all items in cart
    /// </summary>
    public double TotalPrice { get; set; }
}
