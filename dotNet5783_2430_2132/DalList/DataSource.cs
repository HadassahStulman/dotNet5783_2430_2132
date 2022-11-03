
using DO;


namespace Dal;

internal static class DataSource
{
    /// <summary>
    /// class manages unique ID numbers
    /// </summary>
    internal static class Config
    {
        private static int idNewProduct = 100000;
        private static int idNewOrder = 100000;
        private static int idNewOrderItem = 100000;
        /// <summary>
        /// return ID for new product
        /// </summary>
        /// <returns>int</returns>
        public static int getIdNewP() { idNewProduct += 1; return idNewProduct; }
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
    /// <summary>
    /// list of products
    /// </summary>
    internal static List<DO.Products> productsList=new List<DO.Products>();
    /// <summary>
    /// list of orders
    /// </summary>
    internal static List<DO.Order> orderList = new List<DO.Order>();
    /// <summary>
    /// list of order items
    /// </summary>
    internal static List<DO.OrderItem> orderItemList= new List<DO.OrderItem>();
    /// <summary>
    /// adding product to list
    /// </summary>
    /// <param name="p">void</param>
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
    private static void s_Initialize() 
    {
        for (int i = 0; i <20; i++)
        {  }
    }

}
