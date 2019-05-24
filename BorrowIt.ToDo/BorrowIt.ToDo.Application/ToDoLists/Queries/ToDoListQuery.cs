using System;
using BorrowIt.Common.Infrastructure.Abstraction.Queries;

namespace BorrowIt.ToDo.Application.ToDoLists.Queries
{
    public class ToDoListQuery : IQuery
    {
        public Guid? Id { get; set; }
        public string Name { get; set; }
        public Guid? UserId { get; set; }
        public int[] Statuses { get; set; }
    }
}