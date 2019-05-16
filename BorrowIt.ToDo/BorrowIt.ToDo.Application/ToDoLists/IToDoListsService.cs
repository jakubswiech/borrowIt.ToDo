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
        Task StartListAsync(Guid listId);
        Task HoldListAsync(Guid listId);
        Task CancelListAsync(Guid listId);
        Task DeleteListAsync(Guid listId);
        Task CompleteListAsync(Guid listId);
        Task StartTaskAsync(Guid listId, Guid taskId);
        Task HoldTaskAsync(Guid listId, Guid taskId);
        Task CancelTaskAsync(Guid listId, Guid taskId);
        Task DeleteTaskAsync(Guid listId, Guid taskId);
        Task CompleteTaskAsync(Guid listId, Guid taskId);
        Task StartSubTaskAsync(Guid listId, Guid taskId, Guid subTaskId);
        Task HoldSubTaskAsync(Guid listId, Guid taskId, Guid subTaskId);
        Task CancelSubTaskAsync(Guid listId, Guid taskId, Guid subTaskId);
        Task DeleteSubTaskAsync(Guid listId, Guid taskId, Guid subTaskId);
        Task CompleteSubTaskAsync(Guid listId, Guid taskId, Guid subTaskId);
    }
}