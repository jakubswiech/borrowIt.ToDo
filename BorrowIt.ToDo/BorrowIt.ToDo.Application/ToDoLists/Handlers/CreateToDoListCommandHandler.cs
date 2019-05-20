using System.Threading.Tasks;
using AutoMapper;
using BorrowIt.Common.Exceptions;
using BorrowIt.Common.Infrastructure.Abstraction;
using BorrowIt.ToDo.Application.ToDoLists.Commands;
using BorrowIt.ToDo.Domain.Model.ToDoList;
using BorrowIt.ToDo.Domain.Model.ToDoList.DataStructures;

namespace BorrowIt.ToDo.Application.ToDoLists.Handlers
{
    public class CreateToDoListCommandHandler : ICommandHandler<CreateToDoListCommand>
    {
        private readonly IToDoListsService _toDoListsService;
        private readonly IMapper _mapper;

        public CreateToDoListCommandHandler(IToDoListsService toDoListsService, 
            IMapper mapper)
        {
            _toDoListsService = toDoListsService;
            _mapper = mapper;
        }
        
        public async Task HandleAsync(CreateToDoListCommand command)
        {
            var dataStructure = _mapper.Map<ListDataStructure>(command);
            await _toDoListsService.AddListAsync(dataStructure);
        }
    }
    
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
                case ToDoListStatus.InProgress : await _toDoListsService.StartListAsync(command.ListId);
                    break;
                case ToDoListStatus.Done : await _toDoListsService.CompleteListAsync(command.ListId);
                    break;
                case ToDoListStatus.OnHold : await _toDoListsService.HoldListAsync(command.ListId);
                    break;
                case ToDoListStatus.Cancelled : await _toDoListsService.CancelListAsync(command.ListId);
                    break;
                case ToDoListStatus.Archived : await _toDoListsService.DeleteListAsync(command.ListId);
                    break;
                default: throw new BusinessLogicException("incorrect_status");
            }
            
        }
    }
    
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
                case ToDoListStatus.InProgress : await _toDoListsService.StartTaskAsync(command.ListId, command.TaskId);
                    break;
                case ToDoListStatus.Done : await _toDoListsService.CompleteTaskAsync(command.ListId, command.TaskId);
                    break;
                case ToDoListStatus.OnHold : await _toDoListsService.HoldTaskAsync(command.ListId, command.TaskId);
                    break;
                case ToDoListStatus.Cancelled : await _toDoListsService.CancelTaskAsync(command.ListId, command.TaskId);
                    break;
                case ToDoListStatus.Archived : await _toDoListsService.DeleteTaskAsync(command.ListId, command.TaskId);
                    break;
                default: throw new BusinessLogicException("incorrect_status");
            }
            
        }
    }
    
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
                case ToDoListStatus.InProgress : await _toDoListsService.StartSubTaskAsync(command.ListId, command.TaskId, command.SubTaskId);
                    break;
                case ToDoListStatus.Done : await _toDoListsService.CompleteSubTaskAsync(command.ListId, command.TaskId, command.SubTaskId);
                    break;
                case ToDoListStatus.OnHold : await _toDoListsService.HoldSubTaskAsync(command.ListId, command.TaskId, command.SubTaskId);
                    break;
                case ToDoListStatus.Cancelled : await _toDoListsService.CancelSubTaskAsync(command.ListId, command.TaskId, command.SubTaskId);
                    break;
                case ToDoListStatus.Archived : await _toDoListsService.DeleteSubTaskAsync(command.ListId, command.TaskId, command.SubTaskId);
                    break;
                default: throw new BusinessLogicException("incorrect_status");
            }
            
        }
    }
}