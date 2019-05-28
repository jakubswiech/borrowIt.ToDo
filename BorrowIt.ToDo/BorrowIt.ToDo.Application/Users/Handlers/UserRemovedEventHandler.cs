using System.Threading;
using System.Threading.Tasks;
using BorrowIt.Common.Rabbit.Abstractions;
using BorrowIt.ToDo.Application.ToDoLists;
using BorrowIt.ToDo.Infrastructure.Inboud.Messages;

namespace BorrowIt.ToDo.Application.Users.Handlers
{
    public class UserRemovedEventHandler : IMessageHandler<UserRemovedEvent>
    {
        public UserRemovedEventHandler(IUsersService usersService, IToDoListsService doListsService)
        {
            _usersService = usersService;
            _toDoListsService = doListsService;
        }

        private readonly IUsersService _usersService;
        private readonly IToDoListsService _toDoListsService;
        
        public async Task HandleMessageAsync(UserRemovedEvent message, CancellationToken token)
        {
            await _toDoListsService.DeleteAllUserLists(message.Id);
            await _usersService.RemoveUserAsync(message.Id);
        }
    }
}