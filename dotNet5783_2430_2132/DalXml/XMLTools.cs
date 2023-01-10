using System.Xml.Linq;
using System.Xml.Serialization;
using static Dal.DataSource.Config;
namespace Dal;

internal class XMLTools
{
    public static string dir = @"..\xml\";

    static XMLTools()
    {
        if (!Directory.Exists(dir))
            Directory.CreateDirectory(dir);
    }

    public static void LoadData(out XElement xelement, string path)
    {
        try
        {
            xelement = XElement.Load(dir + path);
        }
        catch (Exception ex)
        {
            throw new DO.XMLFileLoadException("File upload problem", ex);
        }
    }

    #region config ID managment
    static string configPath = "Config.xml";
    private static XElement element = new XElement("Config");
    /// <summary>
    /// return ID for new order
    /// </summary>
    /// <returns>int</returns>
    public static int getIdNewO()
    {
        XMLTools.LoadData(out element, configPath);
        XElement OrderId = element.Element("Config")!.Element("IdOrder")!;
        OrderId.Value = (Convert.ToInt32(OrderId.Value) + 1).ToString();
        element.Save(configPath);
        return (Convert.ToInt32(OrderId.Value));
    }
    /// <summary>
    /// return ID for new order item
    /// </summary>
    /// <returns>int</returns>
    public static int getIdNewOI()
    {
        XMLTools.LoadData(out element, configPath);
        XElement OrderItemId = element.Element("Config")!.Element("IdOrderItem")!;
        OrderItemId.Value = (Convert.ToInt32(OrderItemId.Value) + 1).ToString();
        element.Save(configPath);
        return (Convert.ToInt32(OrderItemId.Value));
    }
    #endregion 


    #region serialize functions
    /// <summary>
    /// saves a list to xml
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="list"></param>
    /// <param name="filePath"></param>
    /// <exception cref="DO.XMLFileLoadException"></exception>
    public static void SaveListToXML<T>(List<T> list, string filePath)
    {
        try
        {
            FileStream file = new FileStream(dir + filePath, FileMode.Create);
            XmlSerializer x = new XmlSerializer(list.GetType());
            x.Serialize(file, list);
            file.Close();

        }
        catch (Exception ex) { throw new DO.XMLFileLoadException($"fail to create xml file: {filePath}", ex); }
    }

    /// <summary>
    /// loads a list from xml
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="filePath"></param>
    /// <returns>List<T>?</returns>
    /// <exception cref="DO.XMLFileLoadException"></exception>
    public static List<T> LoadListFromXML<T>(string filePath) where T : struct
    {
        try
        {
            string fullFilePath = dir + filePath;
            if (!File.Exists(fullFilePath))
                return new();
            XmlSerializer x = new XmlSerializer(typeof(List<T>));
            using FileStream file = new FileStream(fullFilePath, FileMode.Open);
             List<T> list = (List<T>)x.Deserialize(file) ?? new();
            return list;
        }
        catch (Exception ex) { throw new DO.XMLFileLoadException($"fail to load xml file: {filePath}", ex); }
    }
    #endregion
}
