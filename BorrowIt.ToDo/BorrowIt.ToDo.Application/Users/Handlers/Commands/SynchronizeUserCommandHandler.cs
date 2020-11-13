using System;
using System.Threading.Tasks;
using AutoMapper;
using BorrowIt.Common.Infrastructure.Abstraction;
using BorrowIt.ToDo.Application.Users.Commands;
using BorrowIt.ToDo.Domain.Model.Users;

namespace BorrowIt.ToDo.Application.Users.Handlers.Commands
{
    public class SynchronizeUserCommandHandler : ICommandHandler<SynchronizeUserCommand>
    {
        private readonly IUsersService _usersService;
        private readonly IMapper _mapper;

        public SynchronizeUserCommandHandler(IUsersService usersService, IMapper mapper)
        {
            _usersService = usersService ?? throw new ArgumentNullException(nameof(usersService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task HandleAsync(SynchronizeUserCommand command)
        {
            var dataStructure = _mapper.Map<UserDataStructure>(command);
            if (await _usersService.UserExists(dataStructure.Id))
            {
                await _usersService.UpdateUserAsync(dataStructure);
            }
            else
            {
                await _usersService.AddUserAsync(dataStructure);
            }
        }
    }
}
