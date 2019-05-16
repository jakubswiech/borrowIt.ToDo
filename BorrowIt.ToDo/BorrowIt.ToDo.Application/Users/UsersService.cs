using System;
using System.Threading.Tasks;
using BorrowIt.Common.Exceptions;
using BorrowIt.ToDo.Domain.Model.Users;
using BorrowIt.ToDo.Infrastructure.Repositories.Users;

namespace BorrowIt.ToDo.Application.Users
{
    public class UsersService : IUsersService
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserFactory _userFactory;

        public UsersService(IUserRepository userRepository, IUserFactory userFactory)
        {
            _userRepository = userRepository;
            _userFactory = userFactory;
        }
        
        public async Task AddUserAsync(UserDataStructure userDataStructure)
        {
            var user = await _userRepository.GetAsync(userDataStructure.Id);
            if (user != null)
            {
                throw new BusinessLogicException("user_exists");
            }

            user = _userFactory.Create(userDataStructure);
            await _userRepository.CreateAsync(user);
        }

        public async Task UpdateUserAsync(UserDataStructure userDataStructure)
        {
            var user = await _userRepository.GetAsync(userDataStructure.Id);
            if (user == null)
            {
                throw new BusinessLogicException("user_not_found");
            }
            
            user.Update(userDataStructure.UserName);
            await _userRepository.UpdateAsync(user);
        }

        public async Task<bool> UserExists(Guid id)
            => await _userRepository.GetAsync(id) != null;
    }
}