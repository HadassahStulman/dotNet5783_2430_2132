using System.Xml.Linq;
using System.Xml.Serialization;
using static Dal.DataSource.Config;

namespace Dal;

public class XMLTools
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
    private static XElement configElement = new XElement("Config");
    /// <summary>
    /// return ID for new order
    /// </summary>
    /// <returns>int</returns>
    public static int getIdNewO()
    {
        try
        {
            XMLTools.LoadData(out configElement, configPath);
            XElement OrderId = configElement.Elements().First(item => item.Name == "IdOrder")!;
            int id = Convert.ToInt32(OrderId.Value) + 1;
            configElement.Element("IdOrder")!.SetValue(id);
            configElement.Save(dir + configPath);
            return (Convert.ToInt32(OrderId.Value));
        }
        catch (Exception ex) { throw ex; }
    }
    /// <summary>
    /// return ID for new order item
    /// </summary>
    /// <returns>int</returns>
    public static int getIdNewOI()
    {
        XMLTools.LoadData(out configElement, configPath);
        XElement OrderId = configElement.Elements().First(item => item.Name == "IdOrderItem")!;
        int id = Convert.ToInt32(OrderId.Value) + 1;
        configElement.Element("IdOrderItem")!.SetValue(id);
        configElement.Save(dir + configPath);
        return (Convert.ToInt32(OrderId.Value));
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
            if (!File.Exists(dir + filePath)) return new List<T>();
            {
                List<T> list;
                XmlSerializer x = new XmlSerializer(typeof(List<T>));
                FileStream file = new FileStream(dir + filePath, FileMode.Open);
                list = (List<T>)x.Deserialize(file);
                file.Close();
                return list;
            }
        }
        catch (Exception ex) { throw ex; }
    }
    #endregion
}
