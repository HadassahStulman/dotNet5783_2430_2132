
using DO;
using static Dal.DataSource;
using static Dal.DataSource.Config;
using System.Collections.Generic;
using System.Collections;
using DalApi;

namespace Dal;

internal class DalProduct:IProduct
{
    /// <summary>
    /// Adding a new product to list. If product (to add) allready exists then throw error.
    /// </summary>
    /// <returns>int</returns>
    /// <param name="p"></param>
    /// <exception cref="Exception"></exception>
 
    public int Add(Product p)
    {
        if (ProductList.Contains(p))
            throw new AlreadyExistingException();
        ProductList.Add(p);
        return p.ID;
    }
    /// <summary>
    /// Deleteing product from list. If product (to delete) does not exists then throw error.
    /// </summary>
    /// <param name="p"></param>
    /// <exception cref="Exception"></exception>
    public void Delete(int pID)
    {
        bool flag= false;
        for (int i = 0; i < ProductList.Count; i++)
            if (ProductList[i].ID == pID)
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
        bool flag = false;
        for (int i= 0; i<ProductList.Count(); i++)
            if (ProductList[i].ID == p.ID)
            {
                ProductList[i] = p;
                flag = true;
                break;
            }
        if (!flag)
            throw new NotExistingException();
    }
    /// <summary>
    /// recieves Uniqe ID for identifing the product and returns product object
    /// </summary>
    /// <param name="p"></param>
    /// <returns>Product</returns>
    public Product GetByID(int id)
    {
        bool flag = false;
        int i = 0;
        for (; i < ProductList.Count(); i++)
            if (ProductList[i].ID == id)
            {
                flag = true;
                break;
            }
        if (!flag)
            throw new NotExistingException();
        return ProductList[i];

    }

    /// <summary>
    /// return list of all Product
    /// </summary>
    /// <returns>IEnumerable<Product></Product></returns>
    public IEnumerable<Product> GetList()
    { 
        List<Product> Product = new List<Product>(ProductList);
        return Product;
    }

    /// <summary>
    /// check if the id already exist in other items
    /// </summary>
    /// <param name="id"></param>
    /// <returns>bool</returns>
    public bool isIDUniqe(int id)
    {
        for (int i = 0; i < ProductList.Count; i++) 
            if (ProductList[i].ID == id)
                return false;
        return true;
    }       
}
