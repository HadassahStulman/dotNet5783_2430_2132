
using DO;
using static Dal.DataSource;
using System.Collections.Generic;
using System.Collections;

namespace Dal;

public class DalProducts
{
    public void Add(Products p)
    {
        if (productsList.Contains(p))
            throw new Exception("product already exists");
        productsList.Add(p);
    }
    public void Delete(Products p)
    {
        if (!productsList.Contains(p))
            throw new Exception("product does not exist");
        productsList.Remove(p);
    }
    public void Update(Products p)
    {
        for(int i= 0; i<productsList.Count(); i++)
            if (productsList[i].ID == p.ID)
            {
                productsList[i] = p;
                break;
            }
    }
    public int GetID(Products p) { return p.ID; }
    public IEnumerable getList()
    {
        return productsList;
    }
}
