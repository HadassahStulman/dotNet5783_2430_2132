using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalApi
{
    /// <summary>
    /// interface for all entities behavior
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ICrud<T> where T : struct
    {
        /// <summary>
        /// base function for adding new Item
        /// </summary>
        /// <param name="entity"></param>
        /// <returns>int</returns>
        public int Add(T entity);

        /// <summary>
        /// base function for deleting a Item
        /// </summary>
        /// <param name="id"></param>
        public void Delete(int id);

        /// <summary>
        /// base function for updating an Item
        /// </summary>
        /// <param name="entity"></param>
        public void Update(T entity);

        /// <summary>
        /// base function for geting all list of items
        /// </summary>
        /// <returns>IEnumerable<typeparamref name="T"/></returns>
        public IEnumerable<T?> GetList(Func<T?, bool>? conditon = null);


        /// <summary>
        /// return all object if func returns true
        /// </summary>
        /// <param name="func"></param>
        /// <returns>T</returns>
        public T? GetIf(Func<T?, bool> func);
    }
}
