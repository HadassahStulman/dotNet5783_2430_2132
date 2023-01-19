
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
            brush = Brushes.PaleVioletRed;
        else if (status == BO.Enums.OrderStatus.OrderShipped)
            brush = Brushes.LightYellow ;
        else
            brush = Brushes.LightGreen ;
        return brush;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }

}



