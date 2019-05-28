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
        Task AddTaskAsync(TaskDataStructure dataStructure, Guid listId, Guid userId);
        Task UpdateTaskAsync(TaskDataStructure dataStructure, Guid listId, Guid userId);
        Task AddSubTaskAsync(SubTaskDataStructure dataStructure, Guid listId, Guid taskId, Guid userId);
        Task UpdateSubTaskAsync(SubTaskDataStructure dataStructure, Guid listId, Guid taskId, Guid userId);
        Task<bool> ToDoListExists(Guid id);
        Task StartListAsync(Guid listId, Guid userId);
        Task HoldListAsync(Guid listId, Guid userId);
        Task CancelListAsync(Guid listId, Guid userId);
        Task DeleteListAsync(Guid listId, Guid userId);
        Task CompleteListAsync(Guid listId, Guid userId);
        Task StartTaskAsync(Guid listId, Guid taskId, Guid userId);
        Task HoldTaskAsync(Guid listId, Guid taskId, Guid userId);
        Task CancelTaskAsync(Guid listId, Guid taskId, Guid userId);
        Task DeleteTaskAsync(Guid listId, Guid taskId, Guid userId);
        Task CompleteTaskAsync(Guid listId, Guid taskId, Guid userId);
        Task StartSubTaskAsync(Guid listId, Guid taskId, Guid subTaskId, Guid userId);
        Task HoldSubTaskAsync(Guid listId, Guid taskId, Guid subTaskId, Guid userId);
        Task CancelSubTaskAsync(Guid listId, Guid taskId, Guid subTaskId, Guid userId);
        Task DeleteSubTaskAsync(Guid listId, Guid taskId, Guid subTaskId, Guid userId);
        Task CompleteSubTaskAsync(Guid listId, Guid taskId, Guid subTaskId, Guid userId);
        Task DeleteAllUserLists(Guid userId);
    }
}