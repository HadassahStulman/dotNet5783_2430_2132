
using DO;

namespace Dal;

internal static class DataSource
{
    /// <summary>
    /// class manages unique ID numbers
    /// </summary>
    internal static class Config
    {
        private static int idNewOrder = 100000;
        private static int idNewOrderItem = 100000;

        /// <summary>
        /// return ID for new order
        /// </summary>
        /// <returns>int</returns>
        public static int getIdNewO() { idNewOrder += 1; return idNewOrder; }
        /// <summary>
        /// return ID for new order item
        /// </summary>
        /// <returns>int</returns>
        public static int getIdNewOI() { idNewOrderItem += 1; return idNewOrderItem; }
    }

    public static readonly Random rnd = new Random();

    private static string[] CookBookName = { "cookBook", "cookBook1", "cookBook2", "cookBook3", "cookBook4", "cookBook5" };
    private static string[] ToddlerBookName = {"Dr. Sues", "Grumpy Monkey", "The Gruffalo", "Night Night Farm", "Hiccupotamus", "I Love You To The Moon And Back", "Magic School Bus"};
    private static string[] ReligiousBookName = { "Bible", "Talmud Set", "Mishnayot", ""};
    private static string[] ReadingBookName = {"The Lord Of The Rings 1", "The Lord Of The Rings 2", "The Lord Of The Rings 3", "Divergent", "Harry Potter - Deathly hollows", "Code Breaker", "The Duches Hunt", "White Fang" , "Anne"};
    private static string[] textBookName = { "calculuse 1", "Programming For Fun", "Basic Fisicis", ""};

    private static string[] customerName = { "Esther Nusbacher","Malka Cohen","yaffa Levi", "Hadassah", "Ayala", "Sam", "Shlomo", "Yael", "Yoni", "Beth", "Daniel", "Ishay" };
    private static string[] customerAdress = {"Nachal Refa'im", "Nachal Dolev","Nachal Ein Gedi","Nachal Shimshone","Nachal Katlav" };


    /// <summary>
    /// list of products
    /// </summary>
    internal static List<Products> productsList = new List<Products>();
    /// <summary>
    /// list of orders
    /// </summary>
    internal static List<Order> orderList = new List<Order>();
    /// <summary>
    /// list of order items
    /// </summary>
    internal static List<OrderItem> orderItemList = new List<OrderItem>();
    
    /// <summary>
    /// adding product to list
    /// </summary>
    /// <param name="p">void</param>
    static DataSource()
    {
        s_Initialize();
    }


    private static void addProduct(Products p) { productsList.Add(p); }
    /// <summary>
    /// adding order to list
    /// </summary>
    /// <param name="o">void</param>
    private static void addOrder(Order o) { orderList.Add(o); }
    /// <summary>
    /// adding order item to list
    /// </summary>
    /// <param name="oi">void</param>
    private static void addOrderItem(OrderItem oi) { orderItemList.Add(oi); }

    /// <summary>
    /// initailize data lists with random elements
    /// </summary>
    private static void s_Initialize()
    {
        for (int i = 0; i < 10; i++) // initalize product list
        {
            Products p = new Products();
            DalProducts dp = new DalProducts();
            int id = rnd.Next(10000, 99999); // random id number of 6 digits
            while (!dp.isIDUniqe(id)) // generates new id until id is uniqe
                id = rnd.Next(10000, 99999);
            p.ID = id;
            int cat = rnd.Next(1, 6); // random category
            // choose a name from a random category
            switch (cat)
            {
                case 1:
                    p.Name = CookBookName[i];
                    break;
                case 2:
                    p.Name = ToddlerBookName[i];
                    break;
                case 3:
                    p.Name = ReligiousBookName[i];
                    break;
                case 4:
                    p.Name = ReadingBookName[i];
                    break;
                case 5:
                    p.Name = textBookName[i];
                    break;
                default:
                    break;
            }
            p.Category=(Enums.Category)cat;
            p.Price = (double) rnd.Next(45, 200);  // random price
            if (i < 9)
                p.InStock = rnd.Next(1, 50); // in stock
            else
                p.InStock = 0; // one product won't be in stock
            productsList.Add(p); // add new item to list
        }
        for (int i = 0; i < 20; i++) // initialize order list
        {
            Order o = new Order();
            o.ID = DataSource.Config.getIdNewO(); // id from config
            o.CustomerName = customerName[i];// customer name
            o.CustomerEmail = customerName[i] + "@gmail.com"; // customer Email
            o.CustomerAdress = customerAdress[i % 5] + "_" + i;// costumer adress
            DateTime d= DateTime.Now.AddDays(rnd.Next(1, 30)*-1); // random date in the past month
            o.OrderDate = d;
            o.ShipDate = DateTime.MinValue; // minimal date for orders that weren't shiped yet
            o.DeliveryDate= DateTime.MinValue; // minimal date for orders that weren't delivered yet
            if (i > 8)
            {
                o.ShipDate = d.AddDays(5); // shiping date
                if (i > 15)
                    o.DeliveryDate = d.AddDays(10); // delivary date
            }
            orderList.Add(o);
        }
        for (int i= 0; i < 20; i++)
        {
            int amount = rnd.Next(1, 5); // random amount of products for each order
            for(int j=0; j<amount; j++)
            {
                OrderItem oi = new OrderItem(); // new order item
                oi.ID = DataSource.Config.getIdNewO();
                oi.OrderId = orderList[i].ID; 
                int ranP = rnd.Next(0, 10);
                oi.ProductId = productsList[ranP].ID; // random product
                oi.Price = productsList[ranP].Price; // random price according to product list
                oi.Amount = rnd.Next(1,6); // random amount of copies
                orderItemList.Add(oi);
            }
        }
    }

}

