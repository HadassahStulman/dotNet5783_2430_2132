
using System.Reflection;

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
            str +=  item.Name +
            ": " + item.GetValue(t, null) + "\n";
        return str;
    }

}
