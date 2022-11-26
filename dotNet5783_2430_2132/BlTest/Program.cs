

using System.Collections.Generic;
using System.Security.Cryptography;

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


    /// <summary>
    /// sub menu product
    /// </summary>
    private static void manageProduct()
    {

        bool flag = true;
        while (flag)
        {
            Console.WriteLine(@"enter: 1 for getting a disply of all orders
       2 for getting an order description according to ID
       3 for updating an orders shipping date
       4 for updating an orders delivery date 
       5 for tracking an order
       6 for manager updating amount of order
       0 for returning back to the main menu ");
            int ch1;
            int.TryParse(Console.ReadLine(), out ch1); // converts the input to integer
            switch (ch1)
            {
                case 0: flag = false; break; // back to main menu
                case 1: PrindAllOrder(); break;
                case 2: PrintOrderDescription(); break;
                case 3: UpdateShipping(); break;
                case 4: UpdateDelivery(); break;
                case 5: TrackOrder(); break;
                case 6: ManagerUpdat(); break;
                default: // back to sub menu
                    break;
            }
        }

    }

    /// <summary>
    /// printing a description of all orders on disply
    /// </summary>
    private static void PrindAllOrder()
    {
        IEnumerable<BO.OrderForList> ofl = Bl.Order.GetList();
        foreach(BO.OrderForList oi in ofl) {
            Console.WriteLine(oi);
        }
    }

    /// <summary>
    /// printing an order description according to ID
    /// </summary>
    private static void PrintOrderDescription()
    {
        Console.WriteLine("enter ID of order");
        int oID;
        int.TryParse(Console.ReadLine(), out oID);
        BO.Order o = Bl.Order.GetByID(oID);
        Console.WriteLine(o);
    }

    /// <summary>
    ///  manager updates orders shipping date  
    /// </summary>
    private static void UpdateShipping()
    {
        Console.WriteLine("enter ID of order");
        int oID;
        int.TryParse(Console.ReadLine(), out oID);
        BO.Order o = Bl.Order.UpdateShipping(oID);
        Console.WriteLine(o);
    }

    /// <summary>
    ///  manager updates orders delivery date  
    /// </summary>
    private static void UpdateDelivery()
    {
        Console.WriteLine("enter ID of order");
        int oID;
        int.TryParse(Console.ReadLine(), out oID);
        BO.Order o = Bl.Order.UpdateDelivery(oID);
        Console.WriteLine(o);
    }

    /// <summary>
    /// printing tracking order description
    /// </summary>
    private static void TrackOrder()
    {
        Console.WriteLine("enter ID of order");
        int oID;
        int.TryParse(Console.ReadLine(), out oID);
        BO.OrderTracking ot = bl.Order.TrackOrder(oID);
        Console.WriteLine( ot);
    }

    /// <summary>
    /// updating amount of order
    /// FOR MANAGER
    /// </summary>
    private static void ManagerUpdat()
    {
        Console.WriteLine("enter ID of order");
        int oID;
        int.TryParse(Console.ReadLine(), out oID);
        BO.Order o = Bl.Order.ManagerUpdateOrder(oID);
        Console.WriteLine(o);
    }
}