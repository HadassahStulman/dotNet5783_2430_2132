
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
    public static K ConvertToBO<T, K>(this T t, K k)
    {
        object boxedk = k!;
        foreach (PropertyInfo Titem in t?.GetType().GetProperties()!)
        {
            //var Kitem = k.GetType().GetProperties().FirstOrDefault(Kitem => Kitem.Name == Titem.Name);
            var Kitem = k?.GetType().GetProperty(Titem.Name)!;
            if (Kitem != null)
            {
                object obj = Titem.GetValue(t, null)!;
                Kitem.SetValue(boxedk, Convert.ChangeType(obj, Kitem.PropertyType));
                //object objconverted= (typeK.GetProperty(Titem.Name)!)obj;
                //Kitem.SetValue(boxedk,obj , null);
            }

        }
        return (K)boxedk;
    }
}
/*
111111
TextBooks
hahha
100
12
*/
