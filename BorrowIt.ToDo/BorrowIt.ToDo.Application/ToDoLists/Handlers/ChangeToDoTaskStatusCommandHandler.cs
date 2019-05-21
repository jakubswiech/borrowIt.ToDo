using System.Threading.Tasks;
using BorrowIt.Common.Exceptions;
using BorrowIt.Common.Infrastructure.Abstraction;
using BorrowIt.ToDo.Application.ToDoLists.Commands;
using BorrowIt.ToDo.Domain.Model.ToDoList;

namespace BorrowIt.ToDo.Application.ToDoLists.Handlers
{
    public class ChangeToDoTaskStatusCommandHandler : ICommandHandler<ChangeToDoTaskStatusCommand>
    {
        private readonly IToDoListsService _toDoListsService;

        public ChangeToDoTaskStatusCommandHandler(IToDoListsService toDoListsService)
        {
            _toDoListsService = toDoListsService;
        }
        
        public async Task HandleAsync(ChangeToDoTaskStatusCommand command)
        {
            var status = (ToDoListStatus) command.Status;
            switch (status)
            {
                case ToDoListStatus.InProgress : await _toDoListsService.StartTaskAsync(command.ListId, command.TaskId, command.UserId.Value);
                    break;
                case ToDoListStatus.Done : await _toDoListsService.CompleteTaskAsync(command.ListId, command.TaskId, command.UserId.Value);
                    break;
                case ToDoListStatus.OnHold : await _toDoListsService.HoldTaskAsync(command.ListId, command.TaskId, command.UserId.Value);
                    break;
                case ToDoListStatus.Cancelled : await _toDoListsService.CancelTaskAsync(command.ListId, command.TaskId, command.UserId.Value);
                    break;
                case ToDoListStatus.Archived : await _toDoListsService.DeleteTaskAsync(command.ListId, command.TaskId, command.UserId.Value);
                    break;
                default: throw new BusinessLogicException("incorrect_status");
            }
            
        }
    }
}