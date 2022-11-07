
using DO;
using static Dal.DataSource;
using static Dal.DataSource.Config;
using System.Collections.Generic;
using System.Collections;
using System.Security.Cryptography.X509Certificates;

namespace Dal;

public class DalProducts
{
    /// <summary>
    /// Adding a new product to list. If product (to add) allready exists then throw error.
    /// </summary>
    /// <returns>int</returns>
    /// <param name="p"></param>
    /// <exception cref="Exception"></exception>
    public int Add(Products p)
    {
        if (productsList.Contains(p))
            throw new Exception("product already exists");
        p.ID = getIdNewP();
        productsList.Add(p);
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
        for (int i = 0; i < productsList.Count; i++)
            if (productsList[i].ID == pID)
            {
                productsList.RemoveAt(i);
                flag = true;
            }
        if (!flag)
            throw new Exception("product does not exist");
    }
    /// <summary>
    /// Updating an product in list. If product (to update) does not exist then throw error.
    /// </summary>
    /// <param name="p"></param>
    public void Update(Products p)
    {
        bool flag = false;
        for (int i= 0; i<productsList.Count(); i++)
            if (productsList[i].ID == p.ID)
            {
                productsList[i] = p;
                flag = true;
                break;
            }
        if (!flag)
            throw new Exception("Order item does not exist\n");
    }
    /// <summary>
    /// recieves Uniqe ID for identifing the product and returns product object
    /// </summary>
    /// <param name="p"></param>
    /// <returns>Products</returns>
    public Products GetID(int id)
    {
        for (int i = 0; i < productsList.Count(); i++)
            if (productsList[i].ID == id)
                return productsList[i];
    }

    /// <summary>
    /// return list of all products
    /// </summary>
    /// <returns>IEnumerable</returns>
    public IEnumerable GetList()
    {
        List<Products> products = new List<Products>();
        products.AddRange(productsList);
        return products;
    }

    /// <summary>
    /// check if the id already exist in other items
    /// </summary>
    /// <param name="id"></param>
    /// <returns>bool</returns>
    public bool isIDUniqe(int id)
    {
        for (int i = 0; i < productsList.Count; i++) 
            if (productsList[i].ID == id)
                return true;
        return false;
    }

    /// <summary>
    /// check if the category is legal
    /// </summary>
    /// <returns></returns>
    public bool isCategory(string con)
    {
        if (con == "TextBooks" || con == "CookBooks" || con == "ToddlerBooks" || con == "ReligiousBooks" || con == "ReadingBooks")
            return true;
        return false;
    }         
}
