

namespace BlApi;

/// <summary>
/// interface for product, contains several methods for managing product list
/// </summary>
public interface IProduct
{
    /// <summary>
    /// returns list of all products for display
    /// FOR MANAGER AND CUSTOMER (in catalg)
    /// </summary>
    /// <returns>IEnumerable<BO.ProductForList></BO.ProductForList></returns>
    public IEnumerable<BO.ProductForList?> GetAll(Func<BO.ProductForList?, bool>? condition = null);

    /// <summary>
    /// returns product object for manager according to it's ID
    /// FOR MANAGER
    /// </summary>
    /// <param name="pID"></param>
    /// <returns>BO.Product</returns>
    public BO.Product GetByID(int pID);

    /// <summary>
    /// returns a certain product descrio=ption according to ID and amount in cart
    /// FOR CUSTOMER
    /// </summary>
    /// <param name="pID"></param>
    /// <param name="crt"></param>
    /// <returns>BO.ProductItem</returns>
    public BO.ProductItem GetByID(int pID, BO.Cart crt);

    /// <summary>
    /// FOR MANAGER
    /// recieves new product description and add to product list
    /// </summary>
    /// <param name=""></param>
    public void AddProduct(BO.Product product);

    /// <summary>
    /// delete a product from product list
    /// FOR MANAGER
    /// </summary>
    /// <param name="pID"></param>
    public void DeleteProduct(int pID);

    /// <summary>
    /// update existing product 
    /// FOR MANAGER
    /// </summary>
    /// <param name="p"></param>
    public void UpdateProduct(BO.Product p);

}
