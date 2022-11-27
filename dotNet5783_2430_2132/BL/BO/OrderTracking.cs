
namespace BO;
/// <summary>
/// class for managing order tracking screen
/// </summary>
public class OrderTracking
{
    /// <summary>
    /// order ID
    /// </summary>
    public int? ID { get; set; }
    /// <summary>
    /// order status
    /// </summary>
    public Enums.OrderStatus? Status { get; set; }
    /// <summary>
    /// list of all tracking stages including description and date
    /// </summary>
    public List<Tuple<DateTime, string>>? TrackingStages { get; set; }

    /// <summary>
    /// returns description of order traccking
    /// </summary>
    /// <returns>string</returns>
    public override string ToString()
    {
        string str = $@"
    Order ID: {ID}
    order status: {Status}
    order tracking stages: 
";
        foreach (Tuple<DateTime, string> ts in this.TrackingStages)
            str += ts.ToString()+ "\n";
        return str;
    }
}
