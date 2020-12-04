using CicekSepeti.DataAccess.IRepositories;
using System;
using System.Threading.Tasks;

namespace CicekSepeti.DataAccess.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IShoppingCartRepository ShoppingCartRepository { get; }
        IProductRepository ProductRepository { get; }
        IUserRepository UserRepository { get; }
        Task<int> CommitAsync();
    }
}
