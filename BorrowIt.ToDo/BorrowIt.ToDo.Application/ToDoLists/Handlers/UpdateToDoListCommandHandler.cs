using System.Threading.Tasks;
using AutoMapper;
using BorrowIt.Common.Infrastructure.Abstraction;
using BorrowIt.ToDo.Application.ToDoLists.Commands;
using BorrowIt.ToDo.Domain.Model.ToDoList.DataStructures;

namespace BorrowIt.ToDo.Application.ToDoLists.Handlers
{
    public class UpdateToDoListCommandHandler : ICommandHandler<UpdateToDoListCommand>
    {
        private readonly IToDoListsService _toDoListsService;
        private readonly IMapper _mapper;

        public UpdateToDoListCommandHandler(IToDoListsService toDoListsService, 
            IMapper mapper)
        {
            _toDoListsService = toDoListsService;
            _mapper = mapper;
        }
        
        public async Task HandleAsync(UpdateToDoListCommand command)
        {
            var dataStructure = _mapper.Map<ListDataStructure>(command);
            await _toDoListsService.UpdateListAsync(dataStructure);
        }
    }
}