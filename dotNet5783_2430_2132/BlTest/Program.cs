
using Dal;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using BlImplementation;
using System.Collections.Generic;
using BO;
using BlApi;
using System.Linq.Expressions;

namespace BL;

public class Program
{

    /// <summary>
    /// in order to allow acsses ta all bl methods
    /// </summary>
    private static BlApi.IBl Bl = new BlImplementation.Bl();
    static void Main()
    {
        try
        {
            bool flag = true;
            while (flag)
            {

                Console.WriteLine(@"enter: 1 if you are manager 
       2 if you are customer
       0 to Exit");
                int ch;
                if (!int.TryParse(Console.ReadLine(), out ch)) // converts the input to integer
                    throw new BO.IlegalDataException("Ilegal choice");
                switch (ch)
                {
                    case 1:
                        Manager();
                        break;
                    case 2:
                        Customer();
                        break;
                    case 0:
                        flag = false;
                        break;
                    default:
                        break;
                }

            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }

    }
    private static void Manager()
    {
        bool flag = true;
        while (flag)
        {
            try
            {
                Console.WriteLine(@"enter: 1 for product
       2 for Order
       0 to Exit");
                int ch;
                if (!int.TryParse(Console.ReadLine(), out ch)) // converts the input to integer
                    throw new BO.IlegalDataException("Ilegal choice");
                switch (ch)
                {
                    case 0:
                        flag = false;
                        break;
                    case 1:
                        manageProduct();
                        break;
                    case 2:
                        manageOrderManager();
                        break;
                    default: // back to main menu
                        break;
                }


            }
            catch (Exception ex) { Console.WriteLine(ex); }
        }
    }
    private static void Customer()
    {
        bool flag = true;
        Console.WriteLine("please enter your name, email and adress");
        string? name = Console.ReadLine();
        string? Email = Console.ReadLine();
        string? adress = Console.ReadLine();
        BO.Cart cart = new BO.Cart()
        {
            CustomerName = name,
            CustomerAdress = adress,
            CustomerEmail = Email,
            TotalPrice = 0,
            Items = new List<BO.OrderItem?>()
        };
        while (flag)
        {
            try
            {
                Console.WriteLine(@"enter: 1 for products catalog 
       2 managing cart
       3 for getting Order Description
       0 to Exit");
                int ch;
                if(!int.TryParse(Console.ReadLine(), out ch)) // converts the input to integer
                    throw new BO.IlegalDataException("Ilegal choice");
                switch (ch)
                {
                    case 0:
                        flag = false;
                        break;
                    case 1:
                        GetAllBooks();
                        break;
                    case 2:
                        manageCart(cart);
                        break;
                    case 3:
                        GetOrderDesc();
                        break;
                    default: // back to main menu
                        break;
                }
            }
            catch (Exception ex) { Console.WriteLine(ex); }

        }
    }

    private static void manageProduct()
    {
        Console.WriteLine(@"enter: 1 for adding a new book
       2 for getting a book description according to ID
       3 for getting catalog of all books
       4 for updating an existing book 
       5 for deleting a book
       0 for returning back to the main menu ");
        int ch1;
        int.TryParse(Console.ReadLine(), out ch1); // converts the input to integer
        switch (ch1)
        {
            case 1: AddNewBook(); break;
            case 2: PrintDescriptionMANAGER(); break;
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
        Console.WriteLine("enter the book's uniqe ID number");
        int id;
        if(!int.TryParse(Console.ReadLine(), out id)) // convert id from string to int
            throw new BO.IlegalDataException("Ilegal ID");
        Console.WriteLine("enter the book's category");
        string? cat = Console.ReadLine();
        Console.WriteLine("enter the book's name");
        string? name = Console.ReadLine();
        Console.WriteLine("enter the book's price");
        double price;
        if(!double.TryParse(Console.ReadLine(), out price)) // convert string to double
            throw new BO.IlegalDataException("Ilegal price");
        Console.WriteLine("enter amount of copies in stock");
        int amount;
        if(!int.TryParse(Console.ReadLine(), out amount)) // convert string to int
            throw new BO.IlegalDataException("Ilegal amount");
        BO.Product p = new BO.Product()
        {
            ID = id,
            Category = (BO.Enums.Category?)Enum.Parse(typeof(BO.Enums.Category?), cat),
            Name = name,
            Price = price,
            InStock = amount
        };
        Bl.Product.AddProduct(p); // add p to data list
    }

    /// <summary>
    /// prints a book description
    /// </summary>
    private static void PrintDescriptionMANAGER()
    {
        Console.WriteLine("enter the book's uniqe ID number");
        int id;
        if(!int.TryParse(Console.ReadLine(), out id)) // convert input to int
            throw new BO.IlegalDataException("Ilegal ID");
        Console.WriteLine(Bl.Product.GetByID(id)); // finds the right book, and print the description
    }

    /// <summary>
    /// print description for every book in data list
    /// </summary>
    private static void GetAllBooks()
    {

        IEnumerable<BO.ProductForList?> ie = Bl.Product.GetAll();
        foreach (BO.ProductForList? item in ie) // print every product in list
        {
            Console.WriteLine($"{item}\n");
        }
    }

    /// <summary>
    /// update a product according to the user's input
    /// </summary>
    private static void UpdateBook()
    {
        Console.WriteLine("enter Id number of book you want to update");
        int id;
        if (!int.TryParse(Console.ReadLine(), out id))  // convert input to int
            throw new BO.IlegalDataException("Ilegal ID");
        Console.WriteLine("enter the book's category");
        string? cat = Console.ReadLine();
        Console.WriteLine("enter the book's name");
        string? name = Console.ReadLine();
        Console.WriteLine("enter the book's price");
        double price;
        if (!double.TryParse(Console.ReadLine(), out price)) // convert string to double
            throw new BO.IlegalDataException("Ilegal price");
        Console.WriteLine("enter amount of copies in stock");
        int amount;
        if (!int.TryParse(Console.ReadLine(), out amount)) // convert string to int
            throw new BO.IlegalDataException("Ilegal amount");
        BO.Product p = new BO.Product()
        {
            ID = id,
            Name = name,
            Price = price,
            Category = (BO.Enums.Category?)Enum.Parse(typeof(BO.Enums.Category?), cat), // convert string to enum
            InStock = amount
        };
        Bl.Product.UpdateProduct(p); // updates p in data list;
    }

    /// <summary>
    /// recieves product's id and delete the product from list
    /// </summary>
    private static void DeleteBook()
    {
        Console.WriteLine("enter Id number of book you want to delete");
        int ID;
        if (!int.TryParse(Console.ReadLine(), out ID))  // convert input to int
            throw new BO.IlegalDataException("Ilegal ID");
        Bl.Product.DeleteProduct(ID); // delete product from list
    }
    private static void manageOrderManager()
    {
        bool flag = true;
        while (flag)
        {
            Console.WriteLine(@"enter: 1 for getting all orders description
       2 for getting an order description according to ID
       3 for updating shipping
       4 for updating Delivery 
       5 for Order tracking
       6 for updating Order
       0 for returning back to the main menu ");
            int ch1;
            if (!int.TryParse(Console.ReadLine(), out ch1)) // converts the input to integer
                throw new BO.IlegalDataException("Ilegal choice");
            switch (ch1)
            {
                case 0: flag = false; break;
                case 1: GetAllOrderDesc(); break;
                case 2: GetOrderDesc(); break;
                case 3: UpdateShipping(); break;
                case 4: UpdateDelivery(); break;
                case 5: TrackOrder(); break;
                case 6: UpdateOrder(); break;
                default: // back to sub menu
                    break;
            }
        }

    }
    private static void GetAllOrderDesc()
    {
        IEnumerable<BO.OrderForList?> ie = Bl.Order.GetList();
        foreach (BO.OrderForList? item in ie)
        {
            Console.WriteLine(item);
        }
    }
    private static void GetOrderDesc()
    {
        Console.WriteLine("enter order ID");
        int ID;
        if (!int.TryParse(Console.ReadLine(), out ID)) // converts the input to integer
            throw new BO.IlegalDataException("Ilegal ID");
        Console.WriteLine(Bl.Order.GetByID(ID)); ;// printing description.
    }
    private static void UpdateShipping()
    {
        Console.WriteLine("enter order ID");
        int ID;
        if (!int.TryParse(Console.ReadLine(), out ID)) // converts the input to integer
            throw new BO.IlegalDataException("Ilegal ID");
        Bl.Order.UpdateShipping(ID);
    }
    private static void UpdateDelivery()
    {
        Console.WriteLine("enter order ID");
        int ID;
        if (!int.TryParse(Console.ReadLine(), out ID)) // converts the input to integer
            throw new BO.IlegalDataException("Ilegal ID");
        Bl.Order.UpdateDelivery(ID);
    }
    private static void TrackOrder()
    {
        Console.WriteLine("enter order ID");
        int ID;
        if (!int.TryParse(Console.ReadLine(), out ID)) // converts the input to integer
            throw new BO.IlegalDataException("Ilegal ID");
        Console.WriteLine(Bl.Order.TrackOrder(ID));
    }
    private static void UpdateOrder()
    {
        Console.WriteLine("enter order ID");
        int ID;
        if (!int.TryParse(Console.ReadLine(), out ID)) // converts the input to integer
            throw new BO.IlegalDataException("Ilegal ID");
        Bl.Order.ManagerUpdateOrder(ID);
    }

    /// <summary>
    /// sub cart menu
    /// </summary>
    /// <param name="cart"></param>
    private static void manageCart(BO.Cart cart)
    {
        bool flag = true;
        while (flag)
        {
            Console.WriteLine(@"enter: 1 for adding a product to cart
       2 for updating a product's amount in cart
       3 for orderring all product in shopping cart
       0 for returning back to the main menu ");
            int ch1;
            if (!int.TryParse(Console.ReadLine(), out ch1)) // converts the input to integer
                throw new BO.IlegalDataException("Ilegal choice");
            switch (ch1)
            {
                case 0: flag = false; break;
                case 1: AddToCart(cart); break;
                case 2: UpdateAmountInCart(cart); break;
                case 3: OrderCart(cart); break;
                default: // back to sub menu
                    break;
            }
        }
    }

    /// <summary>
    /// adding a product to cart
    /// </summary>
    /// <param name="cart"></param>
    private static void AddToCart(BO.Cart cart)
    {
        Console.WriteLine("Enter ID of a product that you want to add to shopping cart");
        int pID;
        if (!int.TryParse(Console.ReadLine(), out pID)) // converts the input to integer
            throw new BO.IlegalDataException("Ilegal ID");
        Bl.Cart.AddToCart(cart, pID);
    }

    /// <summary>
    /// updating a product's amount in cart
    /// </summary>
    /// <param name="cart"></param>
    private static void UpdateAmountInCart(BO.Cart cart)
    {
        Console.WriteLine("Enter ID of a product that you want to update");
        int pID;
        if (!int.TryParse(Console.ReadLine(), out pID))// converts the input to integer
            throw new BO.IlegalDataException("Ilegal ID");
        Console.WriteLine("Enter the amount to update");
        int pAmount;
        if (!int.TryParse(Console.ReadLine(), out pAmount)) // converts the input to integer
            throw new BO.IlegalDataException("Ilegal amount");
        Bl.Cart.UpdateAmountInCart(cart, pID, pAmount);
    }

    /// <summary>
    /// orderring all product in shopping cart
    /// </summary>
    /// <param name="cart"></param>
    private static void OrderCart(BO.Cart cart)
    {
        Console.WriteLine("order ID is: " + Bl.Cart.OrderCart(cart));
    }

}