
using DO;
using static Dal.DataSource;
using static Dal.DataSource.Config;
using DalApi;

namespace Dal;

internal class DalProduct : IProduct
{
    /// <summary>
    /// Adding a new product to list. If product (to add) allready exists then throw error.
    /// </summary>
    /// <returns>int</returns>
    /// <param name="p"></param>
    /// <exception cref="Exception"></exception>

    public int Add(Product p)
    {
        if (ProductList.FirstOrDefault(item => item?.ID == p.ID) != null)
            throw new AlreadyExistingException();
        ProductList.Add(p);
        return (int)p.ID;
    }
    /// <summary>
    /// Deleteing product from list. If product (to delete) does not exists then throw error.
    /// </summary>
    /// <param name="p"></param>
    /// <exception cref="Exception"></exception>
    public void Delete(int pID)
    {
        bool flag = false;
        for (int i = 0; i < ProductList.Count; i++)
            if (ProductList[i]?.ID == pID)
            {
                ProductList.RemoveAt(i);
                flag = true;
            }
        if (!flag)
            throw new NotExistingException();
    }
    /// <summary>
    /// Updating an product in list. If product (to update) does not exist then throw error.
    /// </summary>
    /// <param name="p"></param>
    public void Update(Product p)
    {
        var productToUpdate = ProductList.FirstOrDefault(product => (product?.ID ?? 0) == p.ID);
        if (productToUpdate != null)
        {
            ProductList.Remove(productToUpdate);
            ProductList.Add(p);
        }
        else throw new NotExistingException();
    }

    /// <summary>
    /// return list of all Product
    /// </summary>
    /// <returns>IEnumerable<Product></Product></returns>
    public IEnumerable<Product?> GetList(Func<Product?, bool>? condition) => ProductList.Where(product => condition is null ? true : condition(product));

    /// <summary>
    /// returns the first Product in orderList that fulfils the condition
    /// </summary>
    /// <param name="condition"></param>
    /// <returns></returns>
    public Product? GetIf(Func<Product?, bool> condition) => ProductList.FirstOrDefault(condition);

    /// <summary>
    /// check if the id already exist in other products
    /// </summary>
    /// <param name="id"></param>
    /// <returns>bool</returns>
    public static bool isIDUniqe(int id)
    {
        foreach (Product? item in ProductList)
        {
            if (item != null && item?.ID == id)
                return false;
        }
        return true;
    }
}
