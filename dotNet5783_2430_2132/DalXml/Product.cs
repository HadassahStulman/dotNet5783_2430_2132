using DalApi;
using DO;
using System.Xml.Linq;

namespace Dal;

internal class Product : IProduct
{
    private XElement productXml = new XElement("product");
    private string FPath = @"Product.xml";

    /// <summary>
    /// adding product to file
    /// </summary>
    /// <param name="productToAdd"></param>
    /// <returns></returns>
    public int Add(DO.Product productToAdd)
    {
        XMLTools.LoadData(out productXml, XMLTools.dir + FPath);
        XElement elementToAdd = new XElement("product",
            new XElement("ID", productToAdd.ID),
            new XElement("Name", productToAdd.Name),
            new XElement("Price", productToAdd.Price),
            new XElement("Category", productToAdd.Category),
            new XElement("InStock", productToAdd.InStock));
        productXml.Add(elementToAdd);
        productXml.Save(XMLTools.dir + FPath);
        return productToAdd.ID;
    }

    /// <summary>
    /// Deleteing product from file.
    /// </summary>
    /// <param name="id"></param>
    /// <exception cref="DO.NotExistingException"></exception>
    public void Delete(int id)
    {
        XMLTools.LoadData(out productXml, FPath);
        XElement productToDelete = productXml.Elements().FirstOrDefault(item => Convert.ToInt32(item.Element("ID")!.Value) == id) ?? throw new DO.NotExistingException();
        productToDelete.Remove();
        productXml.Save(XMLTools.dir + FPath);
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
    public IEnumerable<DO.Product?> GetList(Func<DO.Product?, bool>? condition = null)
    {
        productXml = new XElement("products");
        XMLTools.LoadData(out productXml, FPath);
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
        return productList.Cast<DO.Product?>(); // cast to list of nullable objects
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

}
