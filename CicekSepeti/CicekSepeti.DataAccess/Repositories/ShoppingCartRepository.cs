using CicekSepeti.DataAccess.IRepositories;
using CicekSepeti.Domain.Entities;
using CicekSepeti.Domain.IContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CicekSepeti.DataAccess.Repositories
{
    public class ShoppingCartRepository : Repository<ShoppingCart>, IShoppingCartRepository
    {
        public ShoppingCartRepository(ICicekSepetiDbContext context)
           : base(context)
        { }

        public async Task<IEnumerable<ShoppingCart>> GetShoppingCartsByUserId(Guid userId)
        {
            return await _context.ShoppingCarts.Where(x => x.UserId == userId).ToListAsync();
        }

        public async Task<ShoppingCart> GetShoppingCartsByUserIdAndProductId(Guid productId, Guid userId)
        {
            return await _context.ShoppingCarts.FirstOrDefaultAsync(x => x.ProductId == productId && x.UserId == userId);
        }
    }
}
