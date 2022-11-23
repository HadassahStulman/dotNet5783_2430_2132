

namespace BL;

public class Program
{
    private BlApi.IBl Bl = new BlApi();
    static void Main()
    {
        try
        {
            bool flag = true;
            while (flag)
            {
                Console.WriteLine(@"enter: 1 for product
       2 for Order
       3 for Cart
       0 to Exit");
                int ch;
                int.TryParse(Console.ReadLine(), out ch); // converts the input to integer
                switch (ch)
                {
                    case 0:
                        flag = false;
                        break;
                    case 1:
                        manageProduct();
                        break;
                    case 2:
                        manageOrder();
                        break;
                    case 3:
                        manageCart();
                        break;
                    default: // back to main menu
                        break;
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
}