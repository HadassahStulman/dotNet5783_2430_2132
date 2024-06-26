﻿
using System.Collections.ObjectModel;
using System.ComponentModel;


namespace BO;

/// <summary>
/// information of a single order
/// </summary>
public class Order : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;

    /// <summary>
    /// order id
    /// </summary>
    public int ID { get; set; }
    /// <summary>
    /// customer's name
    /// </summary>
    public string? CustomerName { get; set; }
    /// <summary>
    /// customer's Email address
    /// </summary>
    public string? CustomerEmail { get; set; }
    /// <summary>
    /// customer's address for delivery
    /// </summary>
    public string? CustomerAddress { get; set; }
    /// <summary>
    /// date of ordering 
    /// </summary>
    public DateTime? OrderDate { get; set; }


    /// <summary>
    /// status of order (if shiped, delivered...) DP
    /// </summary>
    private Enums.OrderStatus status;
    public Enums.OrderStatus Status
    {
        get { return status; }
        set
        {
            status = value;
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs("Status"));
        }
    }


    /// <summary>
    /// date of order confirmation / pament 
    /// </summary>
    public DateTime? PaymentDate { get; set; }


    /// <summary>
    /// shipping date DP
    /// </summary>
    private DateTime? shipDate;
    public DateTime? ShipDate
    {
        get { return shipDate; }
        set
        {
            shipDate = value;
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs("ShipDate"));
        }
    }


    /// <summary>
    /// delivery date DP
    /// </summary>
    private DateTime? deliveryDate;
    public DateTime? DeliveryDate
    {
        get { return deliveryDate; }
        set
        {
            deliveryDate = value;
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs("DeliveryDate"));
        }
    }

    /// <summary>
    /// list of items in order DP and obsevable collection to notify changes in item list
    /// </summary>
    private ObservableCollection<BO.OrderItem>? items;
    public ObservableCollection<BO.OrderItem>? Items
    {
        get { return items; }
        set
        {
            items = value;
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs("Items"));
        }
    }


    /// <summary>
    /// total price of all products in order DP
    /// </summary>
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
    /// returns description of order
    /// </summary>
    /// <returns>string</returns>
    public override string ToString()
    {
        string str = $@"
Order ID={ID}
customer's name: {CustomerName}
customer's Email: {CustomerEmail}
customer's address: {CustomerAddress}
order date: {OrderDate}
order status: {Status}
order payment date: {PaymentDate}
ship date: {ShipDate}
delivary date: {DeliveryDate}
order's total price: {TotalPrice}

    list of order items:

    ";
        if (Items != null)
            str += string.Join("\n", Items);
        return str;
    }
}




