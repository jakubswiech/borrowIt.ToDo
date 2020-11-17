using System;

namespace BorrowIt.ToDo.Domain.Model.ToDoList.DataStructures
{
    public class ListDataStructure
    {
        public ListDataStructure(Guid? id, Guid userId, string name, DateTime finishUntilDate)
        {
            Id = id;
            Name = name;
            FinishUntilDate = finishUntilDate;
            UserId = userId;
        }

        private ListDataStructure()
        {
        }

        public Guid? Id { get; private set; }
        public Guid UserId { get; private set; }
        public string Name { get; private set; }
        public DateTime FinishUntilDate { get; private set; }
    }
}