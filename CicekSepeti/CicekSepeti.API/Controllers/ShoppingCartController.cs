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

        [HttpGet]
        public async Task<ActionResult<ShoppingCartDTO>> GetBasket(Guid userId)
        {
            var baskets = await _shoppingCartService.GetAllShoppingCartsByUserId(userId);

            var basketDto = _mapper.Map<IEnumerable<ShoppingCart>, IEnumerable<ShoppingCartDTO>>(baskets);

            return Json(basketDto);
        }

        [HttpPost]
        public async Task<JsonResult> AddBasketJson([FromBody] ShoppingCartDTO shoppingCartDTO)
        {
            var shoppingCart = _mapper.Map<ShoppingCartDTO, ShoppingCart>(shoppingCartDTO);

            _logger.LogInformation("testlog123");
            
            var newBasket = await _shoppingCartService.AddShoppingCart(shoppingCart);

            return Json(newBasket);
        }
    }
}