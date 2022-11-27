using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalApi
{
    /// <summary>
    /// interface for Order item behavior
    /// </summary>
    public interface IOrderItem:ICrud<OrderItem>
    {
        /// <summary>
        /// return order item according to product ID and order ID.
        /// </summary>
        /// <param name="pID"></param>
        /// <param name="oID"></param>
        /// <returns>OrderItem</returns>
        public OrderItem GetByBothID(int? pID, int? oID);
        /// <summary>
        /// returns list of all order items in specific order
        /// </summary>
        /// <param name="oID"></param>
        /// <returns>IEnumerable<OrderItem></returns>
        public IEnumerable<OrderItem> GetAllItemsInOrder( int? oID);
    }
}
