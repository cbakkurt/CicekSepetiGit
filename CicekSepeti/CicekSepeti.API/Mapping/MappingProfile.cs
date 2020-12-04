using AutoMapper;
using CicekSepeti.API.DTO;
using CicekSepeti.Domain.Entities;

namespace CicekSepeti.API.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Domain to Resource
            CreateMap<ShoppingCart, ShoppingCartDTO>();
            // Resource to Domain
            CreateMap<ShoppingCartDTO, ShoppingCart>();
        }
    }
}
