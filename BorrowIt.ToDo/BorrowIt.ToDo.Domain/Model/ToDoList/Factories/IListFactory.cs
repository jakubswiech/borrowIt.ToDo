using BorrowIt.ToDo.Domain.Model.ToDoList.DataStructures;

namespace BorrowIt.ToDo.Domain.Model.ToDoList.Factories
{
    public interface IListFactory
    {
        ToDoList Create(ListDataStructure dataStructure);
    }
}