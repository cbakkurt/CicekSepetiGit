using AutoMapper;
using CicekSepeti.API.DTO;
using CicekSepeti.Domain.Entities;
using CicekSepeti.Service.IServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CicekSepeti.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShoppingCartController : Controller
    {
        private readonly IShoppingCartService _shoppingCartService;
        private readonly ILogger<ShoppingCartController> _logger;
        private readonly IMapper _mapper;

        public ShoppingCartController(ILogger<ShoppingCartController> logger, IShoppingCartService shoppingCartService, IMapper mapper)
        {
            _logger = logger;
            _shoppingCartService = shoppingCartService;
            _mapper = mapper;
        }

        [HttpGet("GetShoppingCart")]
        public async Task<ActionResult<ShoppingCartDTO>> GetShoppingCart(Guid userId)
        {
            _logger.LogInformation($"{userId} için sepet çağrıldı.");

            var shoppingCart = await _shoppingCartService.GetAllShoppingCartsByUserId(userId);

            _logger.LogInformation($"{userId} için sepet cevap döndü.");

            var shoppingCartDTO = _mapper.Map<IEnumerable<ShoppingCart>, IEnumerable<ShoppingCartDTO>>(shoppingCart);

            _logger.LogInformation($"{userId} için sepet mapping işlemi yapıldı.");

            return Json(shoppingCartDTO);
        }

        [HttpPost("AddShoppingCart")]
        public async Task<JsonResult> AddShoppingCart([FromBody] ShoppingCartDTO shoppingCartDTO)
        {
            _logger.LogInformation($"User: {shoppingCartDTO.UserId} , Ürün :{shoppingCartDTO.ProductId} için sepet ekleme çağrıldı.");

            var shoppingCart = _mapper.Map<ShoppingCartDTO, ShoppingCart>(shoppingCartDTO);

            _logger.LogInformation($"User: {shoppingCartDTO.UserId} , Ürün :{shoppingCartDTO.ProductId} için sepet mapping işlemi yapıldı..");

            var serviceResponse = await _shoppingCartService.AddShoppingCart(shoppingCart);

            _logger.LogInformation($"User: {shoppingCartDTO.UserId} , Ürün :{shoppingCartDTO.ProductId} için servis cevap döndü. Başarılı :{serviceResponse.IsSuccess}.");

            return Json(serviceResponse);
        }
    }
}