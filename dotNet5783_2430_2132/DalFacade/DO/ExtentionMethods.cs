
using System.Reflection;

namespace DO;

 public static class ExtentionMethods
{
        public static string ToStringProperty<T>(this T t)
        {
            string str = "";
            foreach (PropertyInfo item in t.GetType().GetProperties() )
                str += "\n" + item.Name +
                ": " + item.GetValue(t, null);
            return str;
        }

}
