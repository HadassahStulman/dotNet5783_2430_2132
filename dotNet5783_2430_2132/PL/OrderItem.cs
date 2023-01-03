

using System.Collections.Specialized;
using System.ComponentModel;
using System.Windows;

namespace PL;

/// <summary>
/// information of a single item in order or cart
/// </summary>
public class OrderItem: /*DependencyObject*/INotifyPropertyChanged
{
    /// <summary>
    /// item's ID
    /// </summary>
    public int ID { get; set; }
    /// <summary> 
    /// product's name 
    /// </summary>
    public string? Name { get; set; }
    /// <summary>
    /// product's ID 
    /// </summary>
    public int ProductID { get; set; }
    /// <summary>
    /// price of a single product
    /// </summary>
    public double Price { get; set; }
    /// <summary>
    /// amount of copies of the product
    /// </summary>
    //public int Amount { get; set; }


    private double amount;
    public double Amount
    {
        get { return amount; }
        set
        {
            amount = value;
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs("Amount"));
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    private double totalPrice;
    public double TotalPrice
    {
        get { return totalPrice; }
        set { 
            totalPrice = value;
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs("TotalPrice"));
        }
    }


    /// <summary>
    /// total price of item (price of all copies in total)
    /// </summary>
    //public double TotalPrice { get; set; }


    //public double TotalPrice
    //{
    //    get { return (double)GetValue(TotalPriceProperty); }
    //    set { SetValue(TotalPriceProperty, value); }
    //}

    //// Using a DependencyProperty as the backing store for TotalPrice.  This enables animation, styling, binding, etc...
    //public static readonly DependencyProperty TotalPriceProperty =
    //    DependencyProperty.Register("TotalPrice", typeof(double), typeof(OrderItem), new PropertyMetadata(0));



    /// <summary>
    /// convert object to string
    /// </summary>
    /// <returns>string</returns>
    public override string ToString() => DO.ExtentionMethods.ToStringProperty(this);

}
