using System.Threading.Tasks;
using AutoMapper;
using BorrowIt.Common.Infrastructure.Abstraction;
using BorrowIt.ToDo.Application.ToDoLists.Commands;
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
}