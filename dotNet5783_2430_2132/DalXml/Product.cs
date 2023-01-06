using DalApi;
using DO;
using System.Xml.Linq;

namespace Dal;

internal class Product : IProduct
{
    private XElement productXml;
    private string FPath = @"Product.xml";

    /// <summary>
    /// adding product to file
    /// </summary>
    /// <param name="productToAdd"></param>
    /// <returns></returns>
    public int Add(DO.Product productToAdd)
    {
        productXml = new XElement("product",
            new XElement("ID", productToAdd.ID),
            new XElement("Name", productToAdd.Name),
            new XElement("Price", productToAdd.Price),
            new XElement("Category", productToAdd.Category),
            new XElement("InStock", productToAdd.InStock));
        productXml.Save(FPath);
        return productToAdd.ID;

    }

    /// <summary>
    /// Deleteing product from file.
    /// </summary>
    /// <param name="id"></param>
    /// <exception cref="DO.NotExistingException"></exception>
    public void Delete(int id)
    {
        LoadData();
        XElement productToDelete = productXml.Elements().FirstOrDefault(item => Convert.ToInt32(item.Element("ID")!.Value) == id) ?? throw new DO.NotExistingException();
        productToDelete.Remove();
        productXml.Save(FPath);

    }

    /// <summary>
    /// update product in file 
    /// </summary>
    /// <param name="productToUpdate"></param>
    public void Update(DO.Product productToUpdate)
    {
        Delete(productToUpdate.ID);
        Add(productToUpdate);
    }

    /// <summary>
    /// get all products from xml file that fulfills the condtition
    /// </summary>
    /// <param name="condition"></param>
    /// <returns></returns>
    public IEnumerable<DO.Product> GetList(Func<DO.Product?, bool>? condition = null)
    {
        LoadData();
        var productList = from product in productXml.Elements()
                          let newProduct = new DO.Product
                          {
                              ID = Convert.ToInt32(product.Element("ID")!.Value),
                              Name = product.Element("Name")!.Value,
                              Price = Convert.ToDouble(product.Element("Price")!.Value),
                              Category = (DO.Enums.Category)Enum.Parse(typeof(DO.Enums.Category), product.Element("Category")!.Value),
                              InStock = Convert.ToInt32(product.Element("InStock")!.Value)
                          }
                          where condition == null ? true : condition(newProduct)
                          select newProduct;
        return productList;

    }

    /// <summary>
    /// get product from xml file that fulfills the condtition
    /// </summary>
    /// <param name="func"></param>
    /// <returns></returns>
    /// <exception cref="DO.NotExistingException"></exception>
    public DO.Product? GetIf(Func<DO.Product?, bool> func)
    {
        return (GetList(func) ?? throw new DO.NotExistingException()).First();
    }

    private void LoadData()
    {
        try
        {
            productXml = XElement.Load(FPath);
        }
        catch
        {
            Console.WriteLine("File upload problem");
        }
    }

}
