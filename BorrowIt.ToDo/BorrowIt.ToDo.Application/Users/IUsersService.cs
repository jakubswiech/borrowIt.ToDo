using System;
using System.Threading.Tasks;
using BorrowIt.Common.Application.Services;
using BorrowIt.ToDo.Domain.Model.Users;

namespace BorrowIt.ToDo.Application.Users
{
    public interface IUsersService : IService
    {
        Task AddUserAsync(UserDataStructure userDataStructure);
        Task UpdateUserAsync(UserDataStructure userDataStructure);
        Task<bool> UserExists(Guid id);
    }
}