using System;

namespace BorrowIt.ToDo.Domain.Model.ToDoList.DataStructures
{
    public class ListDataStructure
    {
        public ListDataStructure(Guid? id, string name)
        {
            Id = id;
            Name = name;
        }

        public Guid? Id { get; private set; }
        public string Name { get; private set; }
    }
}