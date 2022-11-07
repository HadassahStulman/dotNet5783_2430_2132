
using DO;
using static Dal.DalOrder;
using System.Collections;
using System.Collections.Specialized;
using System.Reflection.PortableExecutable;
using System;
using System.Collections.Generic;
using System.Runtime.Intrinsics.Arm;
using System.Security.Cryptography.X509Certificates;
using static Dal.DalProducts;

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
            DalProducts dp=new DalProducts();
            Console.WriteLine("enter the book's uniqe ID number");
            int num;
            int.TryParse(Console.ReadLine(), out num); // convert id from string to int
            while (!dp.isIDUniqe(num)) // if code is no uniqe 
            {
                Console.WriteLine("ID is not uniqe, please enter a new ID");
                int.TryParse(Console.ReadLine(), out num); // recieving a new code
            }
            p.ID=num; 
            Console.WriteLine("enter the book's category");
            string cat=Console.ReadLine();
            while(!dp.isCategory(cat)) // if category id illegal
            {
                Console.WriteLine("category is not legal, please enter a new category");
                cat = Console.ReadLine();
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
            dp.Add(p); // add p to data list
        }

        /// <summary>
        /// prints a book description
        /// </summary>
        private void PrintDescription()
        {
            Console.WriteLine("enter the book's uniqe ID number");
            int id;
            int.TryParse(Console.ReadLine(), out id); // convert input to int
            DalProducts dp = new DalProducts();
            Console.WriteLine( dp.GetID(id).ToString()); // finds the right book, and print the description
        }

        /// <summary>
        /// print description for every book in data list
        /// </summary>
        private void GetAll()
        {
            DalProducts dp = new DalProducts();
            IEnumerable ie = dp.GetList();
            foreach(Products p in ie) // print desciption of every product in collection
            { Console.WriteLine(p.ToString()); }
        }

        /// <summary>
        /// update a product according to the user's input
        /// </summary>
        private void UpdateBook()
        {
            DalProducts dp = new DalProducts();
            Console.WriteLine("enter Id number of book you want to update");
            int num;
            int.TryParse(Console.ReadLine(), out num);  // convert input to int
            p.ID = num;
            Console.WriteLine("enter the book's category");
            string cat = Console.ReadLine();
            while (!dp.isCategory(cat)) // if category id illegal
            {
                Console.WriteLine("category is not legal, please enter a new category");
                cat = Console.ReadLine();
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
            dp.Update(p); // updates p in data list;
        }

        /// <summary>
        /// recieves product's id and delete the product from list
        /// </summary>
        private void DeleteBook()
        {
            DalProducts dp = new DalProducts();
            Console.WriteLine("enter Id number of book you want to delete");
            int num;
            int.TryParse(Console.ReadLine(), out num);  // convert input to int
            dp.Delete(num); // delete product from list
        }

        private void manageProduct()
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
                case 3: GetAll(); break;
                case 4: UpdateBook(); break;
                case 5: DeleteBook(); break;
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
            Test t=new Test();
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
                        t.manageProduct();
                        break;
                    case 2:
                        t.manageOrder();
                        break;
                    // case 3:
                    default: // back to main menu
                        break;
                }
            }
        }

    }
}
