using ProductStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductStore.Domain.Concrete
{
    public class EFDbContext : DbContext
    {
        public DbSet<Guitar> Guitars { get; set; }
        public DbSet<AdminAccount> AdminAccounts { get; set; }
    }
}
