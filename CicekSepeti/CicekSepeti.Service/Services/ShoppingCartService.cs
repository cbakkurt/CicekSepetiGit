using CicekSepeti.DataAccess.UnitOfWork;
using CicekSepeti.Domain.Entities;
using CicekSepeti.Service.IServices;
using CicekSepeti.Service.ResponseApi;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CicekSepeti.Service.Services
{
    public class ShoppingCartService : IShoppingCartService
    {
        private readonly IUnitOfWork _unitOfWork;
        public ShoppingCartService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public async Task<ResponseModel> AddShoppingCart(ShoppingCart shoppingCart)
        {
            var user = await _unitOfWork.UserRepository.GetByIdAsync(shoppingCart.UserId);
            var product = await _unitOfWork.ProductRepository.GetByIdAsync(shoppingCart.ProductId);

            var valid = await Validations(shoppingCart, user, product);
            if (valid.IsSuccess == false)
            {
                return valid;
            }

            var shoppingCartIsExist = await GetShoppingCartsByUserIdAndProductId(shoppingCart.UserId, shoppingCart.ProductId);

            if (shoppingCartIsExist == null)
            {
                await _unitOfWork.ShoppingCartRepository.AddAsync(shoppingCart);
            }
            else
            {
                shoppingCartIsExist.Count += shoppingCart.Count;
                _unitOfWork.ShoppingCartRepository.Update(shoppingCartIsExist);

            }

            product.Count = product.Count - shoppingCart.Count;
            _unitOfWork.ProductRepository.Update(product);


            await _unitOfWork.CommitAsync();

            return new ResponseModel
            {
                IsSuccess = true,
                Message = "Sepet güncellendi."
            };
        }

        public async Task<IEnumerable<ShoppingCart>> GetAllShoppingCartsByUserId(Guid userId)
        {
            return await _unitOfWork.ShoppingCartRepository.GetShoppingCartsByUserId(userId);
        }

        private async Task<ResponseModel> Validations(ShoppingCart shoppingCart, User user, Product product)
        {
            if (user == null)
            {

                return new ResponseModel
                {
                    IsSuccess = false,
                    Message = "Kullanıcı bulunamadı."
                };
            }

            if (product == null)
            {

                return new ResponseModel
                {
                    IsSuccess = false,
                    Message = "Ürün bulunamadı"
                };
            }

            if (product.Count < shoppingCart.Count)
            {
                return new ResponseModel
                {
                    IsSuccess = false,
                    Message = $"Bu üründen {shoppingCart.Count} adet bulanmamaktadır. Stokta {product.Count} adet ürün vardır."
                };
            }

            return new ResponseModel { IsSuccess = true };
        }

        private async Task<ShoppingCart> GetShoppingCartsByUserIdAndProductId(Guid userId, Guid productId)
        {
            return await _unitOfWork.ShoppingCartRepository.GetShoppingCartsByUserIdAndProductId(productId, userId);
        }

    }
}
