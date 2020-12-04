using CicekSepeti.DataAccess.IRepositories;
using CicekSepeti.Domain.Entities;
using CicekSepeti.Domain.IContext;
namespace CicekSepeti.DataAccess.Repositories
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        public ProductRepository(ICicekSepetiDbContext context)
            : base(context)
        { }
    }
}
