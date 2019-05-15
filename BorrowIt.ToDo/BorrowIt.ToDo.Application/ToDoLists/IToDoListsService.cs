using System;
using System.Threading.Tasks;
using BorrowIt.Common.Application.Services;
using BorrowIt.ToDo.Domain.Model.ToDoList.DataStructures;

namespace BorrowIt.ToDo.Application.ToDoLists
{
    public interface IToDoListsService : IService
    {
        Task AddListAsync(ListDataStructure dataStructure);
        Task UpdateListAsync(ListDataStructure dataStructure);
        Task AddTaskAsync(TaskDataStructure dataStructure, Guid listId);
        Task UpdateTaskAsync(TaskDataStructure dataStructure, Guid listId);
        Task AddSubTaskAsync(SubTaskDataStructure dataStructure, Guid listId, Guid taskId);
        Task UpdateSubTaskAsync(SubTaskDataStructure dataStructure, Guid listId, Guid taskId);
        Task<bool> ToDoListExists(Guid id);
    }
}