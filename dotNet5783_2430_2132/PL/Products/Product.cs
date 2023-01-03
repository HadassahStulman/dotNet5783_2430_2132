
using System.Windows;

namespace PL.Products;

/// <summary>
/// DependencyObject class For binding Product and window
/// </summary>
public class Product : DependencyObject
{
    /// <summary>
    /// product ID
    /// </summary>
    public int ID
    {
        get { return (int)GetValue(IDProperty); }
        set { SetValue(IDProperty, value); }
    }

    // Using a DependencyProperty as the backing store for ID.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty IDProperty =
        DependencyProperty.Register("ID", typeof(int), typeof(Product), new PropertyMetadata(0));


    /// <summary>
    /// product name
    /// </summary>

    public string Name
    {
        get { return (string)GetValue(NameProperty); }
        set { SetValue(NameProperty, value); }
    }

    // Using a DependencyProperty as the backing store for Name.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty NameProperty =
        DependencyProperty.Register("Name", typeof(string), typeof(Product), new PropertyMetadata(0));

    /// <summary>
    ///  product price
    /// </summary>
    public double Price
    {
        get { return (double)GetValue(PriceProperty); }
        set { SetValue(PriceProperty, value); }
    }

    // Using a DependencyProperty as the backing store for Price.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty PriceProperty =
        DependencyProperty.Register("Price", typeof(double), typeof(Product), new PropertyMetadata(0));

    /// <summary>
    /// product category
    /// </summary>
    public Enums.Category Category
    {
        get { return (Enums.Category)GetValue(CategoryProperty); }
        set { SetValue(CategoryProperty, value); }
    }

    // Using a DependencyProperty as the backing store for Category.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty CategoryProperty =
        DependencyProperty.Register("Category", typeof(Enums.Category), typeof(Product), new PropertyMetadata(0));

    /// <summary>
    /// amount of products in stock
    /// </summary>
    public int InStock
    {
        get { return (int)GetValue(InStockProperty); }
        set { SetValue(InStockProperty, value); }
    }

    // Using a DependencyProperty as the backing store for InStock.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty InStockProperty =
        DependencyProperty.Register("InStock", typeof(int), typeof(Product), new PropertyMetadata(0));



}