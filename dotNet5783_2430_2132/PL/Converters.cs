
using System.Windows.Data;
using System.Globalization;
using System;
using System.Drawing;
using System.Windows.Media;
using System.Net.NetworkInformation;

namespace PL;

/// <summary>
/// converts status of order to maching color for background
/// </summary>
public class StatusToColorConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        BO.Enums.OrderStatus status = (BO.Enums.OrderStatus)Enum.Parse(typeof(BO.Enums.OrderStatus), value.ToString()!);
        Brush brush;
        if (status == BO.Enums.OrderStatus.OrderConfirmed)
            brush = Brushes.Red;
        else if (status == BO.Enums.OrderStatus.OrderShipped)
            brush = Brushes.Yellow;
        else
            brush = Brushes.Green;
        return brush;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }

}

/// <summary>
/// convert status of order to progress bar percentage 
/// </summary>
public class StatusToProgressBarConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        int propgressPercent = 0;
        BO.Enums.OrderStatus status = (BO.Enums.OrderStatus)Enum.Parse(typeof(BO.Enums.OrderStatus), value.ToString()!);
        if (status == BO.Enums.OrderStatus.OrderShipped)
            propgressPercent = 100;
        else if (status == BO.Enums.OrderStatus.OrderDelivered)
            propgressPercent = 200;
        return propgressPercent;
    }
    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}

public class AddorderEventArgs: EventArgs
{
    public int OrderID { get; set;}
}

