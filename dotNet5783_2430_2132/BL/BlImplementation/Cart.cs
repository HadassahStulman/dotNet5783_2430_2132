
using BlApi;

namespace BlImplementation;

internal class Cart : ICart
{
    /// <summary>
    /// private field for allowing accsess from BL to Dal
    /// </summary>
    private DalApi.IDal Dal = new Dal.DalList();
    public BO.Cart AddToCart(BO.Cart crt, int pID)
    {
        throw new NotImplementedException();
    }

    public void OrderCart(BO.Cart crt, string name, string email, string adress)
    {
        throw new NotImplementedException();
    }

    public BO.Cart UpdateAmountInCart(BO.Cart crt, int pID, int amount)
    {
        throw new NotImplementedException();
    }
}
