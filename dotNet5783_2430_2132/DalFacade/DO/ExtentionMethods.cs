
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
    /// copy identical properties from one object to another
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="K"></typeparam>
    /// <param name="t"></param>
    /// <param name="k"></param>
    /// <returns>K</returns>
    public static K? ConvertTo<T, K>(this T? source, K? destination)
    {
        object boxedk = destination!;
        foreach (PropertyInfo Titem in source?.GetType().GetProperties()!) // for each property in source object
        {
            var Kitem = destination?.GetType().GetProperty(Titem.Name)!; // destination property
            if (Kitem != null) // property exist in destination object
            {
                object Tvalue = Titem.GetValue(source)!; // value of source property
                Type KitemPropType = Kitem.PropertyType;
                if (Nullable.GetUnderlyingType(Titem.PropertyType) != null) // if source property type is nullable 
                    KitemPropType = Nullable.GetUnderlyingType(KitemPropType)!; //get underline type

                if (Kitem.PropertyType == Titem.PropertyType) // property type is same in source and destinatiom
                    if (Tvalue != null) // if value of property is not null
                        Kitem.SetValue(boxedk, Convert.ChangeType(Tvalue!, KitemPropType), null); // set value in destination property
                    else Kitem.SetValue(boxedk, null, null); // put null in destination property
            }
        }
        return (K)boxedk;
    }
}

