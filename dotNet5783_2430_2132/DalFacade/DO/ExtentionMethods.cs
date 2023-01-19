
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace DO;

/// <summary>
/// class of all template extention methods
/// </summary>
public static class ExtentionMethods
{
    /// <summary>
    /// template method that prints for all properties topics and values
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="t"></param>
    /// <returns>string</returns>
    public static string ToStringProperty<T>(this T t)
    {
        string str = "";
        foreach (PropertyInfo item in t?.GetType().GetProperties()!)
            str += item.Name +
            ": " + item.GetValue(t, null) + "\n";
        return str;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="K"></typeparam>
    /// <param name="t"></param>
    /// <param name="k"></param>
    /// <returns>K</returns>
    public static K? ConvertTo<T, K>(this T? source, K? destination)
    {
        object boxedk = destination!;
        foreach (PropertyInfo Titem in source?.GetType().GetProperties()!)
        {
            var Kitem = destination?.GetType().GetProperty(Titem.Name)!; // destination property
            if (Kitem != null)
            {
                object Tvalue = Titem.GetValue(source)!; // value of source property

                if (Kitem.PropertyType == Titem.PropertyType)
                    Kitem.SetValue(boxedk, Convert.ChangeType(Tvalue!, Kitem.PropertyType), null); // set value in destination property
            }
        }
        return (K)boxedk;
    }
}

