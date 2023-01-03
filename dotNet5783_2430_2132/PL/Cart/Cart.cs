﻿
using System.Collections.Generic;
using System.ComponentModel;

namespace PL.Cart;

public class Cart : INotifyPropertyChanged
{
    /// <summary>
    /// customer's name
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
    /// customer's Email address
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
    /// customer's address
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
    /// list of all items in cart
    /// </summary>
    private List<BO.OrderItem?>? items;
    public List<BO.OrderItem?>? Items
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
    /// total price of all items in cart
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

    public event PropertyChangedEventHandler? PropertyChanged;
}
