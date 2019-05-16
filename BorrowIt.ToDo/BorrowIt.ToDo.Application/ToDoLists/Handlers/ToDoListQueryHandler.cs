using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BorrowIt.Common.Infrastructure.Abstraction;
using BorrowIt.ToDo.Application.ToDoLists.DTOs;
using BorrowIt.ToDo.Application.ToDoLists.Queries;
using BorrowIt.ToDo.Domain.Model.ToDoList;

namespace BorrowIt.ToDo.Application.ToDoLists.Handlers
{
    public class ToDoListQueryHandler : IQueryHandler<ToDoListQuery, ToDoListResultDto>
    {
        private readonly IToDoListDomainRepository _toDoListDomainRepository;
        private readonly IMapper _mapper;

        public ToDoListQueryHandler(IToDoListDomainRepository toDoListDomainRepository,
            IMapper mapper)
        {
            _toDoListDomainRepository = toDoListDomainRepository;
            _mapper = mapper;
        }
        public async Task<ToDoListResultDto> HandleAsync(ToDoListQuery query)
        {
            var toDoLists = await _toDoListDomainRepository.GetAllAsync();

            var dtos = _mapper.Map<IEnumerable<ToDoListDto>>(toDoLists);
            return new ToDoListResultDto() {Items = dtos.ToList() };
        }
    }
}