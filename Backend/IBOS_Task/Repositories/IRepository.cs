using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IBOS_Task.Repositories
{
    public interface IRepository<T> where T : class
    {
        Task<List<T>> GetAll();
        Task<T> Get(int id);
        Task<T> Insert(T entity);
        Task<T> Update(T entity);
        Task<List<T>> GetByQuantity(int quantity);
        Task Delete(int id);
    }
}
