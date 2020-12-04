using CicekSepeti.DataAccess.IRepositories;
using CicekSepeti.DataAccess.Repositories;
using CicekSepeti.Domain.IContext;
using System.Threading.Tasks;

namespace CicekSepeti.DataAccess.UnitOfWork
{
    public class CicekSepetiUnitOfWork : IUnitOfWork
    {
        private readonly ICicekSepetiDbContext _context;
        private ShoppingCartRepository _shoppingCartRepository;
        private ProductRepository _productRepository;
        private UserRepository _userRepository;

        public CicekSepetiUnitOfWork(ICicekSepetiDbContext context)
        {
            this._context = context;
        }

        public IShoppingCartRepository ShoppingCartRepository => _shoppingCartRepository = _shoppingCartRepository ?? new ShoppingCartRepository(_context);

        public IProductRepository ProductRepository => _productRepository = _productRepository ?? new ProductRepository(_context);
        public IUserRepository UserRepository => _userRepository = _userRepository ?? new UserRepository(_context);

        public async Task<int> CommitAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
