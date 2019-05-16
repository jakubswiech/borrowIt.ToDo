using System.Threading.Tasks;
using AutoMapper;
using BorrowIt.Common.Infrastructure.Abstraction;
using BorrowIt.ToDo.Application.ToDoLists.Commands;
using BorrowIt.ToDo.Domain.Model.ToDoList.DataStructures;

namespace BorrowIt.ToDo.Application.ToDoLists.Handlers
{
    public class CreateToDoSubTaskCommandHandler : ICommandHandler<CreateToDoSubTaskCommand>
    {
        private readonly IToDoListsService _toDoListsService;
        private readonly IMapper _mapper;

        public CreateToDoSubTaskCommandHandler(IToDoListsService toDoListsService, 
            IMapper mapper)
        {
            _toDoListsService = toDoListsService;
            _mapper = mapper;
        }
        
        public async Task HandleAsync(CreateToDoSubTaskCommand command)
        {
            var dataStructure = _mapper.Map<SubTaskDataStructure>(command);
            await _toDoListsService.AddSubTaskAsync(dataStructure, command.ListId, command.TaskId);
        }
    }
}