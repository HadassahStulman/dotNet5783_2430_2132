
using BlApi;
using BO;

namespace BlImplementation;

internal class Product : IProduct
{
    /// <summary>
    /// private field for allowing accsess from BL to Dal
    /// </summary>
    private DalApi.IDal Dal = new Dal.DalList();
    public void AddProduct(int pID, string Name, Enums.Category cat, double price, int amountStock)
    {
        throw new NotImplementedException();
    }

    public void DeleteProduct(int pID)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<ProductForList> GetAll()
    {
        throw new NotImplementedException();
    }

    public BO.Product GetByID(int pID)
    {
        throw new NotImplementedException();
    }

    public ProductItem GetByID(int pID, BO.Cart crt)
    {
        throw new NotImplementedException();
    }

    public void UpdateProduct(BO.Product p)
    {
        throw new NotImplementedException();
    }
}
