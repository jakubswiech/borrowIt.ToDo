using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BorrowIt.Common.Infrastructure.Abstraction;
using BorrowIt.ToDo.Application.ToDoLists.DTOs;
using BorrowIt.ToDo.Application.ToDoLists.Queries;
using BorrowIt.ToDo.Application.ToDoLists.ReadModels;
using BorrowIt.ToDo.Domain.Model.ToDoList;

namespace BorrowIt.ToDo.Application.ToDoLists.Handlers
{
    public class ToDoListQueryHandler : IQueryHandler<ToDoListQuery, ToDoListResultDto>
    {
        private readonly IToDoListReadModel _readModel;

        public ToDoListQueryHandler(IToDoListReadModel readModel)
        {
            _readModel = readModel;
        }

        public async Task<ToDoListResultDto> HandleAsync(ToDoListQuery query)
            => await _readModel.GetToDoListResultDto(query);
    }
}