

namespace BlApi;
/// <summary>
/// interface for shopping cart, contains several methods for managing cart
/// </summary>
public interface ICart
{
    /// <summary>
    /// add product to customers shopping cart and returns updated cart
    /// FOR CUSTOMER    
    /// </summary>
    /// <param name="crt"></param>
    /// <param name="pID"></param>
    /// <returns>BO.Cart</returns>
    public BO.Cart AddToCart(BO.Cart crt, int pID);


    /// <summary>
    /// updates amount of copies of a certain product in cart and return updated cart
    /// FOR CUSTOMER    
    /// </summary>
    /// <param name="crt"></param>
    /// <param name="pID"></param>
    /// <param name="amount"></param>
    /// <returns>BO.Cart</returns>
    public BO.Cart UpdateAmountInCart(BO.Cart crt, int pID, int amount);


    /// <summary>
    /// orders all products in shopping cart, and adding the new order to order list
    /// FOR CUSTOMER
    /// </summary>
    /// <param name="crt"></param>
    /// <returns>int</returns>
    public int? OrderCart(BO.Cart crt);


}
