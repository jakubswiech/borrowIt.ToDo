using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BorrowIt.ToDo.Application.ToDoLists.DTOs;
using BorrowIt.ToDo.Application.ToDoLists.Queries;
using BorrowIt.ToDo.Domain.Model.ToDoList;
using BorrowIt.ToDo.Domain.Model.ToDoList.DataStructures;

namespace BorrowIt.ToDo.Application.ToDoLists.ReadModels
{
    public class ToDoListReadModel : IToDoListReadModel
    {
        private readonly IToDoListDomainRepository _toDoListDomainRepository;
        private readonly IMapper _mapper;

        public ToDoListReadModel(IToDoListDomainRepository toDoListDomainRepository, IMapper mapper)
        {
            _toDoListDomainRepository = toDoListDomainRepository;
            _mapper = mapper;
        }

        public async Task<ToDoListResultDto> GetToDoListResultDto(ToDoListQuery query)
        {
            var dataStructureQuery = _mapper.Map<ListQueryDataStructure>(query);
            var results = await _toDoListDomainRepository.GetAllAsync(dataStructureQuery);

            var dtos = _mapper.Map<IEnumerable<ToDoListDto>>(results);

            return new ToDoListResultDto() {Items = dtos.ToList()};
        }
    }
}