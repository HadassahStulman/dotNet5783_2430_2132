using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalApi
{
    /// <summary>
    /// interface for product behavior
    /// </summary>
    public interface IProduct:ICrud<Product>
    {
        public bool isIDUniqe(int? id);
    }
}
