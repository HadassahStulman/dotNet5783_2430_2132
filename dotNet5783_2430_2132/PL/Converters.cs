
using System.Windows.Data;
using System.Globalization;
using System;
using System.Drawing;
using System.Windows.Media;

namespace PL;

public class StatusToColorConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        BO.Enums.OrderStatus status = (BO.Enums.OrderStatus)Enum.Parse(typeof(BO.Enums.OrderStatus), value.ToString()!);
        Brush brush;
        if (status == BO.Enums.OrderStatus.OrderConfirmed)
            brush = Brushes.Red;
        if (status == BO.Enums.OrderStatus.OrderShipped)
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

