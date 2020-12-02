using System;
using System.Collections.Generic;
using BorrowIt.Common.Infrastructure.Abstraction.DTOs;

namespace BorrowIt.ToDo.Application.ToDoLists.DTOs
{
    public class ToDoListDto : IDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime ModifyDate { get; set; }
        public DateTime FinishUntilDate { get; set; }
        public int Status { get; set; }
        public List<ToDoTaskDto> Tasks { get; set; }
    }
}