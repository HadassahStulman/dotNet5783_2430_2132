using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL.Orders;

internal class OrderForList :INotifyPropertyChanged
{
    /// <summary>
    ///  Order ID 
    /// </summary>
    public int ID { get; set; }
    /// <summary>
    /// name of customer 
    /// </summary>
    public string? CustomerName { get; set; }

    /// <summary>
    /// status of order
    /// </summary>
    public Enums.OrderStatus? Status { get; set; }

    /// <summary>
    /// amount of items items in order
    /// </summary>
    public int AmountOfItems { get; set; }
    /// <summary>
    /// total order price
    /// </summary>
    public double TotalPrice { get; set; }

 
    private int percentage;
    public int Percentage
    {
        get { return percentage; }
        set
        {
            percentage = value;
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs("Percentage"));
            }
        }
    }


    public event PropertyChangedEventHandler? PropertyChanged;
    /// <summary>
    /// convert object to string
    /// </summary>
    /// <returns></returns>
    public override string ToString() => DO.ExtentionMethods.ToStringProperty(this);
}
