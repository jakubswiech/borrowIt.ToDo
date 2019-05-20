using System;
using BorrowIt.ToDo.Domain.Model.ToDoList.DataStructures;

namespace BorrowIt.ToDo.Domain.Model.ToDoList.Factories
{
    public class ListFactory : IListFactory
    {
        public ToDoList Create(ListDataStructure dataStructure)
            => new ToDoList(GetOrGenerateId(dataStructure.Id),dataStructure.UserId, dataStructure.Name);

        private Guid GetOrGenerateId(Guid? id)
            => id ?? Guid.NewGuid();
    }
}