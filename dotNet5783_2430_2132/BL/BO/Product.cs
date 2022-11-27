
using System.ComponentModel;

namespace BO;
/// <summary>
/// class for managing products
/// </summary>
public class Product
{
    /// <summary>
    /// product ID
    /// </summary>
    public int? ID { get; set; } 
    /// <summary>
    /// product name
    /// </summary>
    public string? Name { get; set; } 
    /// <summary>
    ///  product price
    /// </summary>
    public double? Price { get; set; } 
    /// <summary>
    /// product category
    /// </summary>
    public Enums.Category? Category { get; set; }
    /// <summary>
    /// amount of products in stock
    /// </summary>
    public int? InStock { get; set; }

    public override string ToString() => $"ID: {ID}\n Name: {Name}\n Price: {Price}\n Category:{Category}\n Amount In Stock:{InStock}\n";
}
