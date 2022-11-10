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
    public interface ICrud<T>
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
        public IEnumerable<T> GetList();

        /// <summary>
        /// base function for getting existing Item by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>T</returns>
        public T GetByID(int id);
    }
}
