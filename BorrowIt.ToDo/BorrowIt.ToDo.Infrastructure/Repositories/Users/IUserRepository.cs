using BorrowIt.Common.Domain.Repositories;
using BorrowIt.ToDo.Domain.Model.Users;
using BorrowIt.ToDo.Infrastructure.Entities.Users;

namespace BorrowIt.ToDo.Infrastructure.Repositories.Users
{
    public interface IUserRepository : IGenericRepository<User, UserEntity>, IRepository
    {
        
    }
}