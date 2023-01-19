

namespace BO;


/// <summary>
/// class for managing products
/// </summary>
public class Product
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
    ///  product price
    /// </summary>
    public double Price { get; set; } 

    /// <summary>
    /// product category
    /// </summary>
    public Enums.Category? Category { get; set; }

    /// <summary>
    /// amount of products in stock
    /// </summary>
    public int InStock { get; set; }


    /// <summary>
    /// convert object to string
    /// </summary>
    /// <returns></returns>
    public override string ToString() => DO.ExtentionMethods.ToStringProperty(this);
}
