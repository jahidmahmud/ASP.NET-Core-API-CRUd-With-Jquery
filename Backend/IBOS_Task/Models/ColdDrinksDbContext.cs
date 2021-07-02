using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IBOS_Task.Models
{
    public class ColdDrinksDbContext:DbContext
    {
        public ColdDrinksDbContext(DbContextOptions<ColdDrinksDbContext> options):base(options)
        {

        } 
        public DbSet<ColdDrinks> ColdDrinks { set; get; }
    }
}
