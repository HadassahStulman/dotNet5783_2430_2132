

using System.Collections.ObjectModel;
using System.ComponentModel;

namespace BO;
public class Cart : INotifyPropertyChanged
{
    /// <summary>
    /// customer's name DP
    /// </summary>
    private string? customerName;
    public string? CustomerName
    {
        get { return customerName; }
        set
        {
            customerName = value;
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs("CustomerName"));
            }
        }
    }

    /// <summary>
    /// customer's Email address DP
    /// </summary>
    private string? customerEmail;
    public string? CustomerEmail
    {
        get { return customerEmail; }
        set
        {
            customerEmail = value;
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs("CustomerEmail"));
            }
        }
    }

    /// <summary>
    /// customer's address DP
    /// </summary>
    private string? customerAddress;
    public string? CustomerAddress
    {
        get { return customerAddress; }
        set
        {
            customerAddress = value;
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs("CustomerAddress"));
            }
        }
    }
    /// <summary>
    /// list of all items in cart DP
    /// </summary>
    private ObservableCollection<BO.OrderItem?>? items;
    public ObservableCollection<BO.OrderItem?>? Items
    {
        get { return items; }
        set
        {
            items = value;
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs("Items"));
            }
        }
    }
    /// <summary>
    /// total price of all items in cart DP
    /// </summary>
    private double totalPrice;
    public double TotalPrice
    {
        get { return totalPrice; }
        set
        {
            totalPrice = value;
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs("TotalPrice"));
            }
        }
    }
    

    /// <summary>
    /// event for property changes
    /// </summary>
    public event PropertyChangedEventHandler? PropertyChanged;


    /// <summary>
    /// ctor
    /// </summary>
    public Cart()
    {
        Items = new ObservableCollection<BO.OrderItem?>();
    }
}
