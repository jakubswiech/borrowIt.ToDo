using System;
using BorrowIt.Common.Infrastructure.Abstraction.DTOs;

namespace BorrowIt.ToDo.Application.ToDoLists.DTOs
{
    public class ToDoSubTaskDto : IDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime ModifyDate { get; set; }
        public int Status { get; set; }
    }
}