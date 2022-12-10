
namespace BO;
/// <summary>
/// class for managing order tracking screen
/// </summary>
public class OrderTracking
{
    /// <summary>
    /// order ID
    /// </summary>
    public int ID { get; set; }
    /// <summary>
    /// order status
    /// </summary>
    public Enums.OrderStatus? Status { get; set; }
    /// <summary>
    /// list of all tracking stages including description and date
    /// </summary>
    public List<Tuple<DateTime?, string?>>? TrackingStages { get; set; }
    public override string ToString() => $@"ID: {ID}
Status: {Status}
Tracking stages: 
{string.Join("\n", TrackingStages ?? throw new NullReferenceException() )}\n";
}
