
using static DO.Enums;

namespace DO;

/// <summary>
/// Structure for a Book
/// </summary>
public struct Product
{
    /// <summary>
    /// Uniqe ID for identifing a book
    /// </summary>
    public int ID { get; set; }
    /// <summary>
    /// name of the book
    /// </summary>
    public string? Name { get; set; }
    /// <summary>
    /// price for book
    /// </summary>
    public double? Price { get; set; }
    /// <summary>
    /// the category of the book
    /// </summary>
    public Category? Category { get; set; }
    /// <summary>
    /// amount of copies in stock 
    /// </summary>
    public int? InStock { get; set; }

    /// <summary>
    /// returns decription of object (book)
    /// </summary>
    /// <returns>string</returns>
    public override string ToString() => $@"
    Product ID={ID}: {Name}
    cateory - {Category}
    Price: {Price}
    Amount in stock: {InStock}";
  
}
