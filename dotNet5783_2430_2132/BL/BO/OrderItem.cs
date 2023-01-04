

using System.ComponentModel;

namespace BO;

/// <summary>
/// information of a single item in order or cart
/// </summary>
public class OrderItem: INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;

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
    private int amount;
    public int Amount
    {
        get { return amount; }
        set
        {
            amount = value;
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs("Amount"));
        }
    }


    /// <summary>
    /// total price of item (price of all copies in total)
    /// </summary>
    //public double TotalPrice { get; set; }
    private double totalPrice;
    public double TotalPrice
    {
        get { return totalPrice; }
        set
        {
            totalPrice = value;
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs("TotalPrice"));
        }
    }


    /// <summary>
    /// convert object to string
    /// </summary>
    /// <returns>string</returns>
    public override string ToString() => DO.ExtentionMethods.ToStringProperty(this);

}
