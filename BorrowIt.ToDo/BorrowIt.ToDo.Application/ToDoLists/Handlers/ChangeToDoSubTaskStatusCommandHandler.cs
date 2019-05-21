using System.Threading.Tasks;
using BorrowIt.Common.Exceptions;
using BorrowIt.Common.Infrastructure.Abstraction;
using BorrowIt.ToDo.Application.ToDoLists.Commands;
using BorrowIt.ToDo.Domain.Model.ToDoList;

namespace BorrowIt.ToDo.Application.ToDoLists.Handlers
{
    public class ChangeToDoSubTaskStatusCommandHandler : ICommandHandler<ChangeToDoSubTaskStatusCommand>
    {
        private readonly IToDoListsService _toDoListsService;

        public ChangeToDoSubTaskStatusCommandHandler(IToDoListsService toDoListsService)
        {
            _toDoListsService = toDoListsService;
        }
        
        public async Task HandleAsync(ChangeToDoSubTaskStatusCommand command)
        {
            var status = (ToDoListStatus) command.Status;
            switch (status)
            {
                case ToDoListStatus.InProgress : await _toDoListsService.StartSubTaskAsync(command.ListId, command.TaskId, command.SubTaskId, command.UserId.Value);
                    break;
                case ToDoListStatus.Done : await _toDoListsService.CompleteSubTaskAsync(command.ListId, command.TaskId, command.SubTaskId, command.UserId.Value);
                    break;
                case ToDoListStatus.OnHold : await _toDoListsService.HoldSubTaskAsync(command.ListId, command.TaskId, command.SubTaskId, command.UserId.Value);
                    break;
                case ToDoListStatus.Cancelled : await _toDoListsService.CancelSubTaskAsync(command.ListId, command.TaskId, command.SubTaskId, command.UserId.Value);
                    break;
                case ToDoListStatus.Archived : await _toDoListsService.DeleteSubTaskAsync(command.ListId, command.TaskId, command.SubTaskId, command.UserId.Value);
                    break;
                default: throw new BusinessLogicException("incorrect_status");
            }
            
        }
    }
}