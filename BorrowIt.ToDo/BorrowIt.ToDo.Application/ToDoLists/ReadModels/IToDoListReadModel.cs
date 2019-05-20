using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Xml.Linq;
using BorrowIt.ToDo.Application.ToDoLists.DTOs;
using BorrowIt.ToDo.Application.ToDoLists.Queries;

namespace BorrowIt.ToDo.Application.ToDoLists.ReadModels
{
    public interface IToDoListReadModel
    {
        Task<ToDoListResultDto> GetToDoListResultDto(ToDoListQuery query);
    }
}