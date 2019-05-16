using System.Threading.Tasks;
using AutoMapper;
using BorrowIt.Common.Infrastructure.Abstraction;
using BorrowIt.ToDo.Application.ToDoLists.Commands;
using BorrowIt.ToDo.Domain.Model.ToDoList.DataStructures;

namespace BorrowIt.ToDo.Application.ToDoLists.Handlers
{
    public class UpdateToDoSubTaskCommandHandler : ICommandHandler<UpdateToDoSubTaskCommand>
    {
        private readonly IToDoListsService _toDoListsService;
        private readonly IMapper _mapper;

        public UpdateToDoSubTaskCommandHandler(IToDoListsService toDoListsService, 
            IMapper mapper)
        {
            _toDoListsService = toDoListsService;
            _mapper = mapper;
        }
        
        public async Task HandleAsync(UpdateToDoSubTaskCommand command)
        {
            var dataStructure = _mapper.Map<SubTaskDataStructure>(command);
            await _toDoListsService.UpdateSubTaskAsync(dataStructure, command.ListId, command.TaskId);
        }
    }
}