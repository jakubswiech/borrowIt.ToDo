using System.Collections.Generic;
using BorrowIt.Common.Infrastructure.Abstraction.DTOs;

namespace BorrowIt.ToDo.Application.ToDoLists.DTOs
{
    public class ToDoListResultDto : IDto
    {
        public List<ToDoListDto> Items { get; set; }
    }
}