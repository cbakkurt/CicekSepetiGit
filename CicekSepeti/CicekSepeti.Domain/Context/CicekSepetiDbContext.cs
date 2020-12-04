using CicekSepeti.Domain.Entities;
using CicekSepeti.Domain.IContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace CicekSepeti.Domain.Context
{
    public class CicekSepetiDbContext : DbContext, ICicekSepetiDbContext
    {
        public CicekSepetiDbContext(DbContextOptions<CicekSepetiDbContext> options)
            : base(options)
        {

        }

        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<ShoppingCart> ShoppingCarts { get; set; }
        public virtual DbSet<User> Users { get; set; }

        public void Dispose()
        {
            base.Dispose();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await base.SaveChangesAsync();
        }

        DbSet<TEntity> ICicekSepetiDbContext.Set<TEntity>() where TEntity : class
        {
            try
            {
                return Set<TEntity>();

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

    }
}
