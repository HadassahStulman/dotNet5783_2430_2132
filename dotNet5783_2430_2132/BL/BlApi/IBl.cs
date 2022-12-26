
namespace BlApi;

/// <summary>
/// main interface BO
/// </summary>
public interface IBl
{
    public IProduct Product { get; }
    public IOrder Order { get; }
    public ICart Cart { get; }


}
