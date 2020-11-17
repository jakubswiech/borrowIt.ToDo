using System.Threading.Tasks;
using BorrowIt.Common.Exceptions;
using BorrowIt.Common.Infrastructure.Abstraction;
using BorrowIt.ToDo.Application.ToDoLists.Commands;
using BorrowIt.ToDo.Domain.Model.ToDoList;

namespace BorrowIt.ToDo.Application.ToDoLists.Handlers
{
    public class ChangeToDoListStatusCommandHandler : ICommandHandler<ChangeToDoListStatusCommand>
    {
        private readonly IToDoListsService _toDoListsService;

        public ChangeToDoListStatusCommandHandler(IToDoListsService toDoListsService)
        {
            _toDoListsService = toDoListsService;
        }
        
        public async Task HandleAsync(ChangeToDoListStatusCommand command)
        {
            var status = (ToDoListStatus) command.Status;
            switch (status)
            {
                case ToDoListStatus.InProgress : await _toDoListsService.StartListAsync(command.ListId, command.UserId);
                    break;
                case ToDoListStatus.Done : await _toDoListsService.CompleteListAsync(command.ListId, command.UserId);
                    break;
                case ToDoListStatus.OnHold : await _toDoListsService.HoldListAsync(command.ListId, command.UserId);
                    break;
                case ToDoListStatus.Cancelled : await _toDoListsService.CancelListAsync(command.ListId, command.UserId);
                    break;
                case ToDoListStatus.Archived : await _toDoListsService.DeleteListAsync(command.ListId, command.UserId);
                    break;
                default: throw new BusinessLogicException("incorrect_status");
            }
            
        }
    }
}