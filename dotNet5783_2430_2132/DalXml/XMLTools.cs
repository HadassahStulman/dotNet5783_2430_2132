using System.Xml.Serialization;

namespace Dal;

internal class XMLTools
{
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
            FileStream file = new FileStream(filePath, FileMode.Create);
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
    public static List<T>? LoadListFromXML<T>(string filePath)
    {
        try
        {
            if (File.Exists(filePath))
            {
                List<T>? list;
                XmlSerializer x = new XmlSerializer(typeof(List<T>));
                FileStream file = new FileStream(filePath, FileMode.Open);
                list = (List<T>?)x.Deserialize(file);
                file.Close();
                return list;
            }
            else return new List<T>();
        }catch(Exception ex) { throw new DO.XMLFileLoadException($"fail to load xml file: {filePath}", ex); }

    }

}
