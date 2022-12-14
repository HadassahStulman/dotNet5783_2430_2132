
using BlApi;

namespace BlImplementation;

internal sealed class Bl : IBl
{
    public IProduct Product => new Product();

    public IOrder Order => new Order();

    public ICart Cart => new Cart();
}

