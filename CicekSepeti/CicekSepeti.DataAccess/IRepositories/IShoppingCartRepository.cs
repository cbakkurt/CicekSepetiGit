using CicekSepeti.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CicekSepeti.DataAccess.IRepositories
{
    public interface IShoppingCartRepository : IRepository<ShoppingCart>
    {
        Task<ShoppingCart> GetShoppingCartsByUserIdAndProductId(Guid productId, Guid userId);
        Task<IEnumerable<ShoppingCart>> GetShoppingCartsByUserId(Guid userId);
    }
   
}
