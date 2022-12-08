
namespace BO;
/// <summary>
/// class for managing product list screen and catalog screen
/// </summary>
public class ProductForList
{   
    /// <summary>
    /// product ID
    /// </summary>
    public int ID { get; set; }    
    /// <summary>
    /// product name
    /// </summary>
    public string? Name { get; set; }
    /// <summary>
    /// product price
    /// </summary>
    public double Price { get; set; }
    /// <summary>
    /// product category
    /// </summary>
    public Enums.Category? Category { get; set; }
    public override string ToString() => $"ID: {ID}\nName: {Name}\nPrice: {Price}\nCategory: {Category}\n";
}
