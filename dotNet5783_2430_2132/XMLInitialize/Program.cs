
using System.Xml.Linq;
using Dal;


public class program
{
    /// <summary>
    /// list of Product
    /// </summary>
    internal static List<DO.Product?> ProductList = new List<DO.Product?>();
    /// <summary>
    /// list of orders
    /// </summary>
    internal static List<DO.Order?> orderList = new List<DO.Order?>();
    /// <summary>
    /// list of order items
    /// </summary>
    internal static List<DO.OrderItem?> orderItemList = new List<DO.OrderItem?>();

    #region helper function
    public static void LoadData(XElement xelement, string path)
    {
        try
        {
            xelement = XElement.Load(@"..\xml\" + path);
        }
        catch (Exception ex)
        {
            throw new DO.XMLFileLoadException("File upload problem", ex);
        }
    }
    /// <summary>
    /// adding product to list
    /// </summary>
    /// <param name="p">void</param>
    private static void addProduct(DO.Product p) { ProductList.Add(p); }
    /// <summary>
    /// adding order to list
    /// </summary>
    /// <param name="o">void</param>
    private static void addOrder(DO.Order o) { orderList.Add(o); }
    /// <summary>
    /// adding order item to list
    /// </summary>
    /// <param name="oi">void</param>
    private static void addOrderItem(DO.OrderItem oi) { orderItemList.Add(oi); }

    /// <summary>
    /// check if id is uniqe
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public static bool isIDUniqe(int id)
    {
        var product = ProductList.FirstOrDefault(product => (product?.ID ?? 0) == id);
        if (product == null)
            return true;
        return false;
    }
    #endregion

    static void Main(string[] args)
    {
        #region static data
        int oiCode = 100000;
        int oCode = 100000;

        XElement? initialize;
        string producPath = @"..\..\..\..\xml\Product.xml";
        string orderPath = @"..\..\..\..\xml\Order.xml";
        string orderItemPath = @"..\..\..\..\xml\OrderItem.xml";

        Random rnd = new Random();

        string[] CookBookName = { "My first cookBook", "Easy cooking", "Amazing Side Dishes", "children Cook", "simple Baking", "Delicious Diserts", "Best Meat", "Fish 5 ways", "The Choclate Book", "Bread For evey day", "Delicate Cakes" };
        string[] ToddlerBookName = { "Dr. Sues", "Grumpy Monkey", "The Gruffalo", "Night Night Farm", "Hiccupotamus", "I Love You To The Moon And Back", "Magic School Bus", "The giving Tree", "Where's spot", "Dear Zoo", "If Animal's Kissed Good Night", "thing 1 Thing 2" };
        string[] ReligiousBookName = { "Bible", "Talmud Set", "Mishnayot", "Sidur", "Sefer Ha'Chinuch", "Pirkey Avot", "Rambam", "Ramban", "Sforno", "Netzor Leshoncha", "Tania" };
        string[] ReadingBookName = { "The Lord Of The Rings 1", "The Lord Of The Rings 2", "The Lord Of The Rings 3", "Divergent", "Harry Potter - Deathly hollows", "Code Breaker", "The Duches Hunt", "White Fang", "Anne", "The Widow", "Seeing Myself" };
        string[] textBookName = { "Calculuse 1", "Programming For Fun", "Basic Fisicis", "Mathematics for first grade", "I love Science", "Advaced Biology", "Basic C++", "Game Theory", "High School Chimistrey", "ABC For Fun", "Fisiology" };

        string[] customerName = { "Esther_Nusbacher", "Malka_Cohen", "yaffa_Levi", "Hadassah_Stulman", "Ayala_Chaim", "Sam_Cowell", "Shlomo_Raviv", "Yael_Levin", "Yoni_Smith", "Beth_Ben", "Daniel_Keys", "Ishay_Erez" };
        string[] CustomerAddress = { "Nachal Refa'im", "Nachal Dolev", "Nachal Ein Gedi", "Nachal Shimshone", "Nachal Katlav", "Nachal Timna", "Nachal Habesor" };
        #endregion

        #region initialize Product list
        DO.Product p = new DO.Product();
        for (int i = 0; i < 10; i++) // initalize product list
        {
            int id = rnd.Next(100000, 999999); // random id number of 6 digits
            while (!isIDUniqe(id)) // generates new id until id is uniqe
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
            p.Category = (DO.Enums.Category)cat;
            p.Price = (double)rnd.Next(45, 200);  // random price
            if (i < 9)
                p.InStock = rnd.Next(1, 50); // in stock
            else
                p.InStock = 0; // one product won't be in stock
            addProduct(p); // add new item to list
        }
        #endregion

        #region initialize order List
        DO.Order o = new DO.Order();
        for (int i = 0; i < 20; i++) // initialize order list
        {
            o.ID = oCode++; // id from config
            o.CustomerName = customerName[i % 11];// customer name
            o.CustomerEmail = o.CustomerName + "@gmail.com"; // customer Email
            o.CustomerAddress = CustomerAddress[i % 7] + "_" + i;// costumer address
            o.ShipDate = null; // minimal date for orders that weren't shiped yet
            o.DeliveryDate = null; // minimal date for orders that weren't delivered yet
            DateTime date = DateTime.Now.AddDays(rnd.Next(-30, -1));
            if (i < 10)
            {
                o.DeliveryDate = date;
                o.ShipDate = date.AddDays(rnd.Next(1, 14) * -1); // ship date maximum two weeks before delivary
                o.OrderDate = date.AddDays(rnd.Next(14, 21) * -1); // order date between a week and 3 weeks before delivary
            }
            else if (i < 15)
            {
                o.ShipDate = date;
                o.OrderDate = date.AddDays(rnd.Next(1, 14) * -1);
            }
            else o.OrderDate = DateTime.Now.AddDays(rnd.Next(-7, -1));
            addOrder(o);
        }
        #endregion

        #region initialize Order Item List
        for (int i = 0; i < 20; i++)
        {
            int amount = rnd.Next(1, 5); // random amount of Product for each order
            int ran = rnd.Next(0, 10);
            for (int j = 0; j < amount; j++)
            {
                int ranP = (ran + j) % 10;
                DO.OrderItem oi = new DO.OrderItem()
                {  // new order item
                    ID = oiCode++,
                    OrderId = orderList[i]?.ID ?? 0,
                    ProductId = ProductList[ranP]?.ID ?? 0, // random product
                    Price = ProductList[ranP]?.Price ?? 0, // random price according to product list
                    Amount = rnd.Next(1, 6) // random amount of copies
                };
                addOrderItem(oi);
            }
        }
        #endregion

        #region upload config to xml
        XElement? configXml;
        string FPath = @"..\..\..\..\xml\Config.xml";

        configXml = new XElement("Config",
            new XElement("IdOrder", oCode),
            new XElement("IdOrderItem", oiCode));
        configXml.Save(FPath);

        #endregion

        #region upload lists to xml files
        initialize = new XElement("Product",
            from pro in ProductList
            select new XElement("Product",
            new XElement("ID", pro?.ID),
            new XElement("Name", pro?.Name),
            new XElement("Price", pro?.Price),
            new XElement("Category", pro?.Category),
            new XElement("InStock", pro?.InStock)));
        initialize.Save(producPath);


        XMLTools.SaveListToXML(orderList, orderPath);
        XMLTools.SaveListToXML(orderItemList, orderItemPath);
        
        #endregion
    }
}










