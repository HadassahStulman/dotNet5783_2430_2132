

namespace BL;

public class Program
{

    /// <summary>
    /// in order to allow acsses ta all bl methods
    /// </summary>
    private BlApi.IBl Bl = new BlImplementation.Bl();
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
    private static void manageProduct()
    {
        Console.WriteLine(@"enter: 1 for adding a new book
       2 for getting a book description for MANAGAER according to ID
       3 for getting a book description for CUSTOMER according to ID
       4 for getting catalog of all books
       5 for updating an existing book 
       6 for deleting a book
       0 for returning back to the main menu ");
        int ch1;
        int.TryParse(Console.ReadLine(), out ch1); // converts the input to integer
        switch (ch1)
        {
            case 1: AddNewBook(); break;
            case 2: PrintDescriptionMANGER(); break;
            case 3: PrintDescriptionCUSTOMER(); break;
            case 4: GetAllBooks(); break;
            case 5: UpdateBook(); break;
            case 6: DeleteBook(); break;
            default:
                break;
        }
    }
}