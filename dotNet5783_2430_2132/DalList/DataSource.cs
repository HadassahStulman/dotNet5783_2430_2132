
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
        public static int getIdNewO() { idNewOrder+=1; return idNewOrder; }
        /// <summary>
        /// return ID for new order item
        /// </summary>
        /// <returns>int</returns>
        public static int getIdNewOI() { idNewOrderItem+=1; return idNewOrderItem; }
    }

    internal static readonly Random rnd = new Random();

    private static string[] CookBookName = { "My first cookBook", "Easy cooking", "Amazing Side Dishes", "children Cook", "simple Baking", "Delicious Diserts", "Best Meat", "Fish 5 ways", "The Choclate Book", "Bread For evey day", "Delicate Cakes" };
    private static string[] ToddlerBookName = { "Dr. Sues", "Grumpy Monkey", "The Gruffalo", "Night Night Farm", "Hiccupotamus", "I Love You To The Moon And Back", "Magic School Bus", "The giving Tree", "Where's spot", "Dear Zoo", "If Animal's Kissed Good Night", "thing 1 Thing 2" };
    private static string[] ReligiousBookName = { "Bible", "Talmud Set", "Mishnayot", "Sidur", "Sefer Ha'Chinuch", "Pirkey Avot", "Rambam", "Ramban", "Sforno", "Netzor Leshoncha", "Tania" };
    private static string[] ReadingBookName = { "The Lord Of The Rings 1", "The Lord Of The Rings 2", "The Lord Of The Rings 3", "Divergent", "Harry Potter - Deathly hollows", "Code Breaker", "The Duches Hunt", "White Fang", "Anne", "The Widow", "Seeing Myself" };
    private static string[] textBookName = { "Calculuse 1", "Programming For Fun", "Basic Fisicis", "Mathematics for first grade", "I love Science", "Advaced Biology", "Basic C++", "Game Theory", "High School Chimistrey", "ABC For Fun", "Fisiology" };

    private static string[] customerName = { "Esther_Nusbacher", "Malka_Cohen", "yaffa_Levi", "Hadassah_Stulman", "Ayala_Chaim", "Sam_Cowell", "Shlomo_Raviv", "Yael_Levin", "Yoni_Smith", "Beth_Ben", "Daniel_Keys", "Ishay_Erez" };
    private static string[] CustomerAddress = { "Nachal Refa'im", "Nachal Dolev", "Nachal Ein Gedi", "Nachal Shimshone", "Nachal Katlav", "Nachal Timna", "Nachal Habesor" };


    /// <summary>
    /// list of Product
    /// </summary>
    internal static List<Product?> ProductList = new List<Product?>();
    /// <summary>
    /// list of orders
    /// </summary>
    internal static List<Order?> orderList = new List<Order?>();
    /// <summary>
    /// list of order items
    /// </summary>
    internal static List<OrderItem?> orderItemList = new List<OrderItem?>();


    static DataSource()
    {
        s_Initialize();
    }

    /// <summary>
    /// adding product to list
    /// </summary>
    /// <param name="p">void</param>
    private static void addProduct(Product p) { ProductList.Add(p); }
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
        Product p = new Product();
        for (int i = 0; i < 10; i++) // initalize product list
        {
            int id = rnd.Next(100000, 999999); // random id number of 6 digits
            while (!DalProduct.isIDUniqe(id)) // generates new id until id is uniqe
                id = rnd.Next(10000, 99999);
            p.ID = id;
            int cat = rnd.Next(0, 5); // random category
            // choose a name from a random category
            switch (cat)
            {
                case 0:
                    p.Name = textBookName[i];
                    break;
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
                default:
                    break;
            }
            p.Category = (Enums.Category)cat;
            p.Price = (double)rnd.Next(45, 200);  // random price
            if (i < 9)
                p.InStock = rnd.Next(1, 50); // in stock
            else
                p.InStock = 0; // one product won't be in stock
            addProduct(p); // add new item to list
        }
        Order o = new Order();
        for (int i = 0; i < 20; i++) // initialize order list
        {
            o.ID = DataSource.Config.getIdNewO(); // id from config
            o.CustomerName = customerName[i % 5];// customer name
            o.CustomerEmail = customerName[i % 5] + "@gmail.com"; // customer Email
            o.CustomerAddress = CustomerAddress[i % 5] + "_" + i;// costumer address
            o.ShipDate = null; // minimal date for orders that weren't shiped yet
            o.DeliveryDate = null; // minimal date for orders that weren't delivered yet
            DateTime date = DateTime.Now.AddDays(rnd.Next(-30, -1));
            if (i < 13)
            {
                o.DeliveryDate = date;
                o.ShipDate = date.AddDays(rnd.Next(1, 14) * -1); // ship date maximum two weeks before delivary
                o.OrderDate = date.AddDays(rnd.Next(14, 21) * -1); // order date between a week and 3 weeks before delivary
            }
            else if (i < 17)
            {
                o.ShipDate = date;
                o.OrderDate = date.AddDays(rnd.Next(1, 14) * -1);
            }
            else o.OrderDate = DateTime.Now.AddDays(rnd.Next(-7, -1));
            addOrder(o);
        }
        for (int i = 0; i < 20; i++)
        {
            int amount = rnd.Next(1, 5); // random amount of Product for each order
            for (int j = 0; j < amount; j++)
            {
                int ranP = rnd.Next(0, 10);
                OrderItem oi = new OrderItem()
                {  // new order item
                    ID = DataSource.Config.getIdNewO(),
                    OrderId = orderList[i]?.ID??0,
                    ProductId = ProductList[ranP]?.ID??0, // random product
                    Price = ProductList[ranP]?.Price??0, // random price according to product list
                    Amount = rnd.Next(1, 6) // random amount of copies
                };
                addOrderItem(oi);
            }
        }
    }

}

