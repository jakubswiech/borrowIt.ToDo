using System;
using System.Threading.Tasks;
using BorrowIt.Common.Infrastructure.Abstraction;
using BorrowIt.ToDo.Application.Users.Commands;

namespace BorrowIt.ToDo.Application.Users.Handlers.Commands
{
    public class RemoveUserCommandHandler : ICommandHandler<RemoveUserCommand>
    {
        private readonly IUsersService _usersService;

        public RemoveUserCommandHandler(IUsersService usersService)
        {
            _usersService = usersService ?? throw new ArgumentNullException(nameof(usersService));
        }

        public async Task HandleAsync(RemoveUserCommand command)
        {
            await _usersService.RemoveUserAsync(command.Id);
        }
    }
}