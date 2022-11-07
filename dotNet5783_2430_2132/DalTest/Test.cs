
using DO;
using static Dal.DalOrder;
using System.Collections.Specialized;
using System.Reflection.PortableExecutable;
using System;
using System.Collections.Generic;

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
        /// <summary>
        /// adding a new order to list.
        /// </summary>
        private void AddNewOrder()
        {
            DalOrder order = new DalOrder();
            o.ID = order.getIdNewO();// id from config
            Console.WriteLine("enter your name");
            o.CustomerName = Console.ReadLine();// receiving name of customer.
            Console.WriteLine("enter your email address");
            o.CustomerEmail = Console.ReadLine(); // receiving email address of customer.
            Console.WriteLine("enter your address");
            o.CustomerAdress = Console.ReadLine();// receiving address of customer.
            o.OrderDate = DateTime.Now; // order date is- current date.
            o.ShipDate = DateTime.Now.AddDays(5); // shipping date is five days from order.
            o.DeliveryDate = DateTime.Now.AddDays(7); // delivery date is seven days from order.
            int newIt = order.Add(o); // adding order to order list.
        }

        /// <summary>
        /// printing description of order that matches this ID.
        /// </summary>
       private void GetOrderDesc()
        {
            Console.WriteLine("enter order ID");
            int ID;
            int.TryParse(Console.ReadLine(), out ID); // converts the input to integer
            DalOrder order = new DalOrder();
            o = order.GetID(ID);// find order that matches this ID.
            o.ToString();// printing description.
        }

        /// <summary>
        /// printing a descroptions of all orders.
        /// </summary>
        private void GetAllOrderDesc()
        {
            DalOrder order = new DalOrder();
            order.PrintAllOrders(); // a method that prints a description of all orders in list.

        }

        /// <summary>
        /// updateing order that matches this ID number.
        /// </summary>
       private void UpdateOrder()
        {
            DalOrder order = new DalOrder();
            Console.WriteLine("enter order ID to update");
            int ID;
            int.TryParse(Console.ReadLine(), out ID); // converts the input to integer
            o = order.GetID(ID); // getting from list order that needs to be updated
            order.Delete(ID); // deleting the Outdated order from list.
            Console.WriteLine("enter updated name");
            o.CustomerName = Console.ReadLine();// receiving updated name of customer.
            Console.WriteLine("enter updated email address");
            o.CustomerEmail = Console.ReadLine(); // receiving updated email address of customer.
            Console.WriteLine("enter updated address");
            o.CustomerAdress = Console.ReadLine();// receiving updated address of customer.
            o.OrderDate = DateTime.Now; // order date is- current date.
            o.ShipDate = DateTime.Now.AddDays(5); // shipping date is five days from order.
            o.DeliveryDate = DateTime.Now.AddDays(7); // delivery date is seven days from order.
            int newIt = order.Add(o); // adding updated order to order list.


        }

        /// <summary>
        /// deleting order from list according to ID.
        /// </summary>
        private void DeleteOrder()
        {
            Console.WriteLine("enter order ID to delete");
            int ID;
            int.TryParse(Console.ReadLine(), out ID); // converts the input to integer
            DalOrder order = new DalOrder();
            order.Delete(ID); // deleting order.
        }

        /// <summary>
        /// sub menu
        /// </summary>
        private void manageOrder()
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
                    case 0:flag = false;break;
                    case 1: AddNewOrder(); break;
                    case 2: GetOrderDesc(); break;
                    case 3: GetAllOrderDesc(); break;
                    case 4: UpdateOrder(); break;
                    case 5: DeleteOrder(); break;
                    default: // back to sub menu
                        break;
                }
            }

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
