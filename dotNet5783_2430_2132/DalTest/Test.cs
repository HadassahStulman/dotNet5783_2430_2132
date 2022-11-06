

using DO;
using System.Collections.Specialized;
using System.Reflection.PortableExecutable;

namespace Dal
{
    internal class Test
    {
        private Products p = new Products();
        private OrderItem oi = new OrderItem();
        private Order o = new Order();

        /// <summary>
        /// adding a new book with user's input details
        /// </summary>
        private void AddNewBook()
        {
            Products book = new Products();
            Console.WriteLine("enter the book's category");
            Console.WriteLine("enter the book's name");
            book.Name = Console.ReadLine();

        }
        private void manageProduct()
        {
            Console.WriteLine(@" enter: 1 for adding a new book
                                                    2 for getting a book description according to ID
                                                    3 for getting descroptions of all books
                                                    4 for updating an existing book 
                                                    5 for deleting a book
                                                    0 for returning back to the main menu ");
            int ch1;
            int.TryParse(Console.ReadLine(), out ch1); // converts the input to integer
            switch (ch1)
            {
                case 1: AddNewBook(); break;
                case 2:
                case 3:
                case 4:
                case 5:
                default:
                    break;
            }
        }

        private void manageOrder()
        {

        }
        public static void main()
        {
            bool flag = true;
            while (flag)
            {
                Console.WriteLine(@"enter: 1 for product
                                           2 for Order
                                           3 for Order Item
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
                    // case 3:
                    default: // back to main menu
                        break;
                }
            }
        }

    }
}
