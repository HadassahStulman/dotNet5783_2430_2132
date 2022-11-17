
namespace BO;
/// <summary>
/// class for managing  product catalog screen
/// </summary>
public class ProductItem
{   /// <summary>
    /// product ID
    /// </summary>
    public int? id { get; set; }  
    /// <summary>
    /// product name
    /// </summary>
    public string? Name { get; set; }
    /// <summary>
    /// product price
    /// </summary>
    public double? Price { get; set; }
    /// <summary>
    /// product category
    /// </summary>
    public Enums.Category? Category { get; set; }
    /// <summary>
    /// amount of product
    /// </summary>
    public int? Amount { get; set; }
    /// <summary>
    /// saves availability of product
    /// </summary>
    public bool? InStock { get; set; }  

}
