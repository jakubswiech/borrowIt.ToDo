using System;

namespace BorrowIt.ToDo.Domain.Model.ToDoList.DataStructures
{
    public class ListQueryDataStructure
    {
        public Guid? Id { get; set; }
        public string Name { get; set; }
        public Guid? UserId { get; set; }
        public int? Status { get; set; }
    }
}