
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
    public static K ConvertToBO<T, K>(this T source, K destination) where T : struct where K : struct
    {
        object boxedk = destination!;
        foreach (PropertyInfo Titem in source.GetType().GetProperties()!)
        {
            var Kitem = destination.GetType().GetProperty(Titem.Name)!; // destination property
            if (Kitem != null)
            {
                Type sourseType = Nullable.GetUnderlyingType(Titem.PropertyType)!;
                Type destinationType = Nullable.GetUnderlyingType(Kitem.PropertyType)!;

                object Tvalue = Titem.GetValue(source, null)!; // value of source property
                if (sourseType==destinationType)
                    Kitem.SetValue(boxedk, Convert.ChangeType(Tvalue!, destinationType), null); // set value in destination property
                if (Titem.GetType().IsEnum)
                    Kitem.SetValue(boxedk, Convert.ChangeType(Enum.ToObject(destinationType, Tvalue), destinationType), null); // set value in destination property
            }

        }
        return (K)boxedk;

        //        static class Copy
        //{
        //    public static Target CopyPropTo<Source, Target>(this Source source, Target target)
        //    {
        //        Dictionary<string, PropertyInfo> propertyInfoTarget = target.GetType().GetProperties()
        //            .ToDictionary(key => key.Name, value => value);

        //        IEnumerable<PropertyInfo> propertyInfoSource = source.GetType().GetProperties();

        //        foreach (var item in propertyInfoSource)
        //        {
        //            if (propertyInfoTarget.ContainsKey(item.Name) && (item.PropertyType == typeof(string) || !item.PropertyType.IsClass))
        //            {
        //                Type typeSource = Nullable.GetUnderlyingType(item.PropertyType);
        //                Type typeTarget = Nullable.GetUnderlyingType(propertyInfoTarget[item.Name].PropertyType);

        //                object value = item.GetValue(source);

        //                if (typeSource is not null && typeTarget is not null)
        //                    value = Enum.ToObject(typeTarget, value);

        //                else if (propertyInfoTarget[item.Name].PropertyType is item.PropertyType)
        //                    propertyInfoTarget[item.Name].SetValue(target, value);
        //            }
        //        }

        //        return target;
        //    }


    }
        public static TEnum ConvertEnum<TEnum>(this Enum source)
    {
        return (TEnum)Enum.Parse(typeof(TEnum), source.ToString(), true);
    }
}

//// Usage
//NewEnumType newEnum = oldEnumVar.ConvertEnum<NewEnumType>()

/*
111111
TextBooks
hahha
100
12
*/
