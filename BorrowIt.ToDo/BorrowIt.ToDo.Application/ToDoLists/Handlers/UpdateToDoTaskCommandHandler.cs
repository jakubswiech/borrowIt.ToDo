using System.Threading.Tasks;
using AutoMapper;
using BorrowIt.Common.Infrastructure.Abstraction;
using BorrowIt.ToDo.Application.ToDoLists.Commands;
using BorrowIt.ToDo.Domain.Model.ToDoList.DataStructures;

namespace BorrowIt.ToDo.Application.ToDoLists.Handlers
{
    public class UpdateToDoTaskCommandHandler : ICommandHandler<UpdateToDoTaskCommand>
    {
        private readonly IToDoListsService _toDoListsService;
        private readonly IMapper _mapper;

        public UpdateToDoTaskCommandHandler(IToDoListsService toDoListsService, 
            IMapper mapper)
        {
            _toDoListsService = toDoListsService;
            _mapper = mapper;
        }
        
        public async Task HandleAsync(UpdateToDoTaskCommand command)
        {
            var dataStructure = _mapper.Map<TaskDataStructure>(command);
            await _toDoListsService.UpdateTaskAsync(dataStructure, command.ListId);
        }
    }
}