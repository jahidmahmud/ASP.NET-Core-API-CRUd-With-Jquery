using IBOS_Task.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IBOS_Task.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ColdDrinksDbContext _context;
        private readonly DbSet<T> _entity;
        public Repository(ColdDrinksDbContext context)
        {
            _context = context;
            _entity = _context.Set<T>();
        }

        public async Task Delete(int id)
        {
             _context.Set<T>().Remove(await Get(id));
            await _context.SaveChangesAsync();
        }

        public async Task<T> Get(int id)
        {
            return await _entity.FindAsync(id);
        }
        public async Task<List<T>> GetByQuantity(int quantity)
        {
            return await ColdDrinksList(quantity) as List<T>;
        }

        public async Task<List<T>> GetAll()
        {
            return await _entity.ToListAsync();
        }

        public async Task<T> Insert(T entity)
        {
           await _context.Set<T>().AddAsync(entity);
           await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<T> Update(T entity)
        {
            _context.Set<T>().Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
        private async Task<List<ColdDrinks>> ColdDrinksList(int quantity)
        {
            return await _context.ColdDrinks.Where(x => x.Quantity < quantity).ToListAsync();
        }
    }
}
