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