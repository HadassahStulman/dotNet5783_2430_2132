
using DO;
using DalApi;
namespace Dal
{
    public class Program
    {
        private static IDal DalList = DalApi.Factory.Get()!;

        /// <summary>
        /// Main program
        /// </summary>
        static void Main()
        {
            try
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
                        case 3:
                            manageOtderItem();
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

        /// <summary>
        /// sub menu product
        /// </summary>
        private static void manageProduct()
        {
            Console.WriteLine(@"enter: 1 for adding a new book
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
                case 2: PrintDescription(); break;
                case 3: GetAllBooks(); break;
                case 4: UpdateBook(); break;
                case 5: DeleteBook(); break;
                default:
                    break;
            }
        }

        /// <summary>
        /// adding a new book with user's input details
        /// </summary>
        private static void AddNewBook()
        {
            Product p = new Product();
            Console.WriteLine("enter the book's uniqe ID number");
            int.TryParse(Console.ReadLine(), out int num); // convert id from string to int
            p.ID = num;
            Console.WriteLine("enter the book's category");
            string cat = Console.ReadLine()!;
            try
            {
                p.Category = (Enums.Category)Enum.Parse(typeof(Enums.Category), cat); // convert cat from string to Enum
            }
            catch (Exception)
            {
                throw new Exception("Ilegal Category");
            }
            Console.WriteLine("enter the book's name");
            p.Name = Console.ReadLine();
            Console.WriteLine("enter the book's price");
            double.TryParse(Console.ReadLine(), out double price); // convert string to double
            p.Price = price;
            Console.WriteLine("enter amount of copies in stock");
            int.TryParse(Console.ReadLine(), out num); // convert string to int
            p.InStock = num;
            DalList.Product.Add(p); // add p to data list
        }

        /// <summary>
        /// prints a book description
        /// </summary>
        private static void PrintDescription()
        {
            Console.WriteLine("enter the book's uniqe ID number");
            int.TryParse(Console.ReadLine(), out int id); // convert input to int
            Console.WriteLine(DalList.Product.GetIf(item=>item?.ID==id)); // finds the right book, and print the description
        }

        /// <summary>
        /// print description for every book in data list
        /// </summary>
        private static void GetAllBooks()
        {

            IEnumerable<Product?> ie = DalList.Product.GetList();
            foreach (Product? item in ie) // printd every product in list
            {
                Console.WriteLine(item);
            }
        }

        /// <summary>
        /// update a product according to the user's input
        /// </summary>
        private static void UpdateBook()
        {
            Product p = new Product();
            Console.WriteLine("enter Id number of book you want to update");
            int num;
            int.TryParse(Console.ReadLine(), out num);  // convert input to int
            p.ID = num;
            Console.WriteLine("enter the book's category");
            string cat = Console.ReadLine()!;
            try
            {
                p.Category = (Enums.Category)Enum.Parse(typeof(Enums.Category), cat); // convert cat from string to Enum
            }
            catch (Exception)
            {
                throw new Exception("Ilegal Category");
            }
            Console.WriteLine("enter the book's name");
            p.Name = Console.ReadLine();
            Console.WriteLine("enter the book's price");
            double price;
            double.TryParse(Console.ReadLine(), out price); // convert string to double
            p.Price = price;
            Console.WriteLine("enter amount of copies in stock");
            int.TryParse(Console.ReadLine(), out num); // convert string to int
            p.InStock = num;
            DalList.Product.Update(p); // updates p in data list;
        }

        /// <summary>
        /// recieves product's id and delete the product from list
        /// </summary>
        private static void DeleteBook()
        {
            Console.WriteLine("enter Id number of book you want to delete");
            int num;
            int.TryParse(Console.ReadLine(), out num);  // convert input to int
            DalList.Product.Delete(num); // delete product from list
        }

        /// <summary>
        /// sub menu order
        /// </summary>
        private static void manageOrder()
        {
            bool flag = true;
            while (flag)
            {
                Console.WriteLine(@" enter: 1 for adding a new order
       2 for getting an order description according to ID
       3 for getting descroptions of all orders
       4 for updating an existing order 
       5 for deleting a order
       0 for returning back to the main menu ");
                int ch1;
                int.TryParse(Console.ReadLine(), out ch1); // converts the input to integer
                switch (ch1)
                {
                    case 0: flag = false; break;
                    case 1: AddNewOrder(); break; // add new order to list
                    case 2: GetOrderDesc(); break; // prints order description
                    case 3: GetAllOrderDesc(); break; // print descriptions of all orders in list
                    case 4: UpdateOrder(); break; // update existing order
                    case 5: DeleteOrder(); break; // update 
                    default: // back to sub menu
                        break;
                }
            }

        }

        /// <summary>
        /// adding a new order to list.
        /// </summary>
        private static void AddNewOrder()
        {
            Order _order = new Order();
            Console.WriteLine("enter your name");
            _order.CustomerName = Console.ReadLine();// receiving name of customer.
            Console.WriteLine("enter your email address");
            _order.CustomerEmail = Console.ReadLine(); // receiving email address of customer.
            Console.WriteLine("enter your address");
            _order.CustomerAdress = Console.ReadLine();// receiving address of customer.
            _order.OrderDate = DateTime.Now; // order date is- current date.
            _order.ShipDate = DateTime.Now.AddDays(5); // shipping date is five days from order.
            _order.DeliveryDate = DateTime.Now.AddDays(7); // delivery date is seven days from order.
            int newID = DalList.Order.Add(_order); // adding order to order list.
            AddOI(newID);
        }

        /// <summary>
        /// printing description of order that matches this ID.
        /// </summary>
        private static void GetOrderDesc()
        {
            Order? _order = new Order?();
            Console.WriteLine("enter order ID");
            int.TryParse(Console.ReadLine(), out int id); // converts the input to integer
            _order = DalList.Order.GetIf(item => item?.ID == id);// find order that matches this ID.
            Console.WriteLine(_order); ;// printing description.
        }

        /// <summary>
        /// printing a descroptions of all orders.
        /// </summary>
        private static void GetAllOrderDesc()
        {
            IEnumerable<Order?> ie = DalList.Order.GetList();
            foreach (Order? item in ie) // printd every product in list
            {
                Console.WriteLine(item);
            }
        }

        /// <summary>
        /// updateing order that matches this ID number.
        /// </summary>
        private static void UpdateOrder()
        {
            Order _order = new();
            Console.WriteLine("enter order ID to update");
            int.TryParse(Console.ReadLine(), out int id); // converts the input to integer
            _order = DalList.Order.GetIf(item => item?.ID == id) ?? throw new ArgumentNullException(); // getting from list order that needs to be updated
            Console.WriteLine("enter updated name");
            _order.CustomerName = Console.ReadLine();// receiving updated name of customer.
            Console.WriteLine("enter updated email address");
            _order.CustomerEmail = Console.ReadLine(); // receiving updated email address of customer.
            Console.WriteLine("enter updated address");
            _order.CustomerAdress = Console.ReadLine();// receiving updated address of customer.
            _order.OrderDate = DateTime.Now; // order date is- current date.
            _order.ShipDate = DateTime.Now.AddDays(5); // shipping date is five days from order.
            _order.DeliveryDate = DateTime.Now.AddDays(7); // delivery date is seven days from order.
            DalList.Order.Update(_order); // adding updated order to order list.
            manageOrderItem(id);
        }

        /// <summary>
        /// deleting order from list according to ID.
        /// </summary>
        private static void DeleteOrder()
        {
            Console.WriteLine("enter order ID to delete");
            int ID;
            int.TryParse(Console.ReadLine(), out ID); // converts the input to integer
            DalList.Order.Delete(ID); // deleting order.
        }


        /// <summary>
        /// function that manages all possible changes in order item list !!!! when updating order !!!!
        /// </summary>
        /// <param name="id"></param>
        private static void manageOrderItem(int id)
        {
            int choice;
            bool flag = true;
            while (flag) // while user wants to continue changing order items
            {
                Console.WriteLine(@"enter: 1 for adding items
       2 for updating items
       3 for deleting items
       0 to finish updating order");
                int.TryParse(Console.ReadLine(), out choice);
                switch (choice)
                {
                    case 1: AddOI(id); break;
                    case 2: UpdateOI(id); break;
                    case 3: DeleteOI(); break;
                    case 0: flag = false; break;
                    default:
                        break;
                }
            }
        }

        /// <summary>
        /// function that manages all possible changes in order item list
        /// </summary>
        private static void manageOtderItem()
        {
            bool flag = true;
            while (flag)
            {
                Console.WriteLine(@"enter: 1 for getting an order Item description according to ID
       2 for getting descroptions of all orders Items
       3 for deleting a order Item
       4 for getting an order Item description according to product ID and order ID 
       5 for getting all order items of a specific order
       0 for returning back to the main menu ");
                int ch1;
                int.TryParse(Console.ReadLine(), out ch1); // converts the input to integer
                switch (ch1)
                {
                    case 0: flag = false; break;
                    case 1: OrderItemDesc(); break; // prints order item description
                    case 2: GetAllOrderItem(); break; // print descriptions of all order items in list
                    case 3: DeleteOI(); break; // delete existing order item
                    case 4: OrderItemDescBy2ID(); break;// prints order idem description by product ID and order ID 
                    case 5: AllItemsInOrder(); break; //  prints all order items description that are in a specific order
                    default: // back to sub menu
                        break;
                }
            }
        }

        /// <summary>
        /// add new order items to order
        /// </summary>
        /// <param name="id"></param>
        private static void AddOI(int id)
        {
            int amOI = 5;
            //OrderItem _orderItem = new OrderItem();
            while (amOI > 4) // while amount of Product in order is more than 4
            {
                Console.WriteLine("enter amount of Product to add to Order, up to 4 Product");
                int.TryParse(Console.ReadLine(), out amOI);
            }
            int pid;
            int amount;
            for (int i = 0; i < amOI; i++) // for every product inorder
            {
                Console.WriteLine("enter product uniqe ID number");
                if (!int.TryParse(Console.ReadLine(), out pid)) // convert input to integer
                    throw new FormatException();
                Product? prod = DalList.Product.GetIf(item => item?.ID == id); // get price of product from product list
                Console.WriteLine("enter amount of copies of " + prod?.Name);
                if (!int.TryParse(Console.ReadLine(), out amount)) // convert input to integer
                    throw new FormatException();
                DalList.OrderItem.Add(new OrderItem()
                {
                    ID = 0,
                    ProductId = pid,
                    OrderId = id,
                    Price = prod?.Price ?? 0,
                    Amount = amount
                });
                ; // add new order item to list
            }
        }

        /// <summary>
        /// updating order items from a certain order
        /// </summary>
        /// <param name="id"></param>
        private static void UpdateOI(int id)
        {
            int amOI = 5;
            while (amOI > 4) // while amount of product to update in order is more than 4
            {
                Console.WriteLine("enter amount of order items to update");
                if (!int.TryParse(Console.ReadLine(), out amOI))
                    throw new FormatException();

            }
            int pid;
            int amount;
            for (int i = 0; i < amOI; i++) // update all order items that the user requested
            {
                Console.WriteLine("enter product uniqe ID number");
                if (!int.TryParse(Console.ReadLine(), out pid)) // convert input to integer
                    throw new FormatException();
                Product? prod = DalList.Product.GetIf(item => item?.ID == id); // get price of product from product list
                Console.WriteLine("enter amount of copies of " + prod?.Name);
                if (!int.TryParse(Console.ReadLine(), out amount)) // convert input to integer
                    throw new FormatException();
                DalList.OrderItem.Add(new OrderItem()
                {
                    ID = 0,
                    ProductId = pid,
                    OrderId = id,
                    Price = prod?.Price ?? 0,
                    Amount = amount
                });
            }
        }

        /// <summary>
        /// deleting order items from a certain order
        /// </summary>
        private static void DeleteOI()
        {
            int amOI = 5;
            while (amOI > 4) // while amount of Product to delete from order is more than 4
            {
                Console.WriteLine("enter amount of order items to delete");
                if (!int.TryParse(Console.ReadLine(), out amOI)) // convert input to integer
                    throw new FormatException();
            }
            int id;
            for (int i = 0; i < amOI; i++) // delete all order items that the user requested
            {
                Console.WriteLine("enter ID of order item to delete");
                if (!int.TryParse(Console.ReadLine(), out id)) // convet input to integer
                    throw new FormatException();
                DalList.OrderItem.Delete(id);
            }
        }

        /// <summary>
        /// printing description of all order items
        /// </summary>
        private static void GetAllOrderItem()
        {
            IEnumerable<OrderItem?> ie = DalList.OrderItem.GetList();
            foreach (OrderItem? item in ie) // printd every product in list
            {
                Console.WriteLine(item);
            }
        }

        /// <summary>
        /// print description of specific order item
        /// </summary>
        private static void OrderItemDesc()
        {
            Console.WriteLine("enter order item ID");
            if (!int.TryParse(Console.ReadLine(), out int id)) // converts the input to integer
                throw new FormatException();
            Console.WriteLine(DalList.OrderItem.GetIf(item=>item?.ID==id)); ;// printing description.
        }

        /// <summary>
        /// print description of specific order item by both IDs
        /// </summary>
        private static void OrderItemDescBy2ID()
        {
            Console.WriteLine("enter product ID");
            int pID;
            if (!int.TryParse(Console.ReadLine(), out pID)) // converts the input to integer
                throw new FormatException();
            Console.WriteLine("enter order ID");
            int oID;
            if (!int.TryParse(Console.ReadLine(), out oID)) // converts the input to integer
                throw new FormatException();
            Console.WriteLine(DalList.OrderItem.GetIf(item => item?.ProductId == pID && item?.OrderId == oID)); ;// printing description.
        }
        /// <summary>
        /// printing list of all order items
        /// </summary>
        private static void AllItemsInOrder()
        {
            Console.WriteLine("enter order ID");
            int ID;
            if (!int.TryParse(Console.ReadLine(), out ID)) // converts the input to integer
                throw new FormatException();
            IEnumerable<OrderItem?> iE = DalList.OrderItem.GetList(item => item?.OrderId == ID); // returns list of order item
            foreach (OrderItem? item in iE) // printing description
                Console.WriteLine(item);
        }
    }
}
