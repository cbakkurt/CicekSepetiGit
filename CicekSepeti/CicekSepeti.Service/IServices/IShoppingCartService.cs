using CicekSepeti.Domain.Entities;
using CicekSepeti.Service.ResponseApi;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CicekSepeti.Service.IServices
{
    public interface IShoppingCartService
    {
        Task<ResponseModel> AddShoppingCart(ShoppingCart shoppingCart);

        Task<IEnumerable<ShoppingCart>> GetAllShoppingCartsByUserId(Guid userId);
    }
}
