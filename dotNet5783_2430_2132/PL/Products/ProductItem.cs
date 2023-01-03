
using System;
using System.ComponentModel;
using System.Windows;

namespace PL.Products;

public class ProductItem : INotifyPropertyChanged
{
    private int id;
    public int ID
    {
        get { return id; }
        set
        {
            id = value;
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs("ID"));
            }
        }
    }

    private string? name;
    public string? Name
    {
        get { return name; }
        set
        {
            name = value;
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs("Name"));
            }
        }
    }


    private double price;
    public double Price
    {
        get { return price; }
        set
        {
            price = value;
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs("Price"));
            }
        }
    }



    private Enums.Category category;
    public Enums.Category Category
    {
        get { return category; }
        set
        {
            category = value;
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs("Category"));
            }
        }
    }


    private int amount;
    public int Amount
    {
        get { return amount; }
        set
        {
            amount = value;
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs("Amount"));
            }
        }
    }



    private bool inStock;
    public bool InStock
    {
        get { return inStock; }
        set
        {
            inStock = value;
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs("InStock"));
            }
        }
    }

    public event PropertyChangedEventHandler PropertyChanged;
}
