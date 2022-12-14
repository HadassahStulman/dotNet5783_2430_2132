

namespace BlApi;
public class Factory
{
    public static IBl Get()
    {
        return new BlImplementation.Bl();
    }
}
