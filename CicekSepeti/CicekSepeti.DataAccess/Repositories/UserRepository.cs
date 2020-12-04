using CicekSepeti.DataAccess.IRepositories;
using CicekSepeti.Domain.Entities;
using CicekSepeti.Domain.IContext;

namespace CicekSepeti.DataAccess.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(ICicekSepetiDbContext context)
           : base(context)
        { }
    }
}
