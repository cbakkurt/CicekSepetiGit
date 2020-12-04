using CicekSepeti.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace CicekSepeti.Domain.IContext
{
    public interface ICicekSepetiDbContext : IDisposable
    {
        DbSet<TEntity> Set<TEntity>() where TEntity : class;
        DbSet<Product> Products { get; set; }
        DbSet<ShoppingCart> ShoppingCarts { get; set; }
        DbSet<User> Users { get; set; }

        Task<int> SaveChangesAsync();
    }
}
