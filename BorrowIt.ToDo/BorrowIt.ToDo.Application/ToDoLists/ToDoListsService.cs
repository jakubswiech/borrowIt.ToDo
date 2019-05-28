using System;
using System.Linq;
using System.Threading.Tasks;
using BorrowIt.Common.Exceptions;
using BorrowIt.ToDo.Domain.Model.ToDoList;
using BorrowIt.ToDo.Domain.Model.ToDoList.DataStructures;
using BorrowIt.ToDo.Domain.Model.ToDoList.Factories;
using BorrowIt.ToDo.Domain.Model.Users;
using BorrowIt.ToDo.Infrastructure.Repositories.Users;

namespace BorrowIt.ToDo.Application.ToDoLists
{
    public class ToDoListsService : IToDoListsService
    {
        private readonly IToDoListDomainRepository _toDoListRepository;
        private readonly IUserRepository _userRepository;
        private readonly IListFactory _toDoListFactory;

        public ToDoListsService(IToDoListDomainRepository toDoListRepository, IListFactory toDoListFactory, IUserRepository userRepository)
        {
            _toDoListRepository = toDoListRepository;
            _toDoListFactory = toDoListFactory;
            _userRepository = userRepository;
        }
        
        public async Task AddListAsync(ListDataStructure dataStructure)
        {
            var user = await GetUserOrThrowAsync(dataStructure.UserId);
            var toDoList = _toDoListFactory.Create(dataStructure);

            await _toDoListRepository.PersistAsync(toDoList);
        }


        public async Task UpdateListAsync(ListDataStructure dataStructure)
        {
            var toDoList = await GetOneOrThrowAsync(dataStructure.Id?? new Guid());
            
            ValidateUserAsync(toDoList.UserId, dataStructure.UserId);
            toDoList.Update(dataStructure.Name);

            await _toDoListRepository.PersistAsync(toDoList);
        }

        public async Task AddTaskAsync(TaskDataStructure dataStructure, Guid listId, Guid userId)
        {
            var toDoList = await GetOneOrThrowAsync(listId);
            
            ValidateUserAsync(toDoList.UserId, userId);
            
            toDoList.AddTask(dataStructure);

            await _toDoListRepository.PersistAsync(toDoList);
        }

        public async Task UpdateTaskAsync(TaskDataStructure dataStructure, Guid listId, Guid userId)
        {
            var toDoList = await GetOneOrThrowAsync(listId);
            
            ValidateUserAsync(toDoList.UserId, userId);
            
            toDoList.UpdateTask(dataStructure);

            await _toDoListRepository.PersistAsync(toDoList);
        }

        public async Task AddSubTaskAsync(SubTaskDataStructure dataStructure, Guid listId, Guid taskId, Guid userId)
        {
            var toDoList = await GetOneOrThrowAsync(listId);
            
            ValidateUserAsync(toDoList.UserId, userId);
            var task = toDoList.Tasks.SingleOrDefault(x => x.Id == taskId);
            if (task == null)
            {
                throw new BusinessLogicException("task_not_found");
            }
            
            task.AddSubTask(dataStructure);
            await _toDoListRepository.PersistAsync(toDoList);
        }

        public async Task UpdateSubTaskAsync(SubTaskDataStructure dataStructure, Guid listId, Guid taskId, Guid userId)
        {
            var toDoList = await GetOneOrThrowAsync(listId);
            
            ValidateUserAsync(toDoList.UserId, userId);
            var task = toDoList.Tasks.SingleOrDefault(x => x.Id == taskId);
            if (task == null)
            {
                throw new BusinessLogicException("task_not_found");
            }
            
            task.UpdateSubTask(dataStructure);
            await _toDoListRepository.PersistAsync(toDoList);
        }

        public async Task<bool> ToDoListExists(Guid id)
            => await _toDoListRepository.GetOneAsync(id) != null;

        public async Task StartListAsync(Guid listId, Guid userId)
        {
            var list = await GetOneOrThrowAsync(listId);
            
            ValidateUserAsync(list.UserId, userId);
            list.StartProgress();
            await _toDoListRepository.PersistAsync(list);
        }

        public async Task HoldListAsync(Guid listId, Guid userId)
        {
            var list = await GetOneOrThrowAsync(listId);
            
            ValidateUserAsync(list.UserId, userId);
            list.Hold();
            await _toDoListRepository.PersistAsync(list);
        }

        public async Task CancelListAsync(Guid listId, Guid userId)
        {
            var list = await GetOneOrThrowAsync(listId);
            
            ValidateUserAsync(list.UserId, userId);
            list.Cancel();
            await _toDoListRepository.PersistAsync(list);
        }

        public async Task DeleteListAsync(Guid listId, Guid userId)
        {
            var list = await GetOneOrThrowAsync(listId);
            
            ValidateUserAsync(list.UserId, userId);
            list.MarkAsArchived();
            await _toDoListRepository.PersistAsync(list);
        }

        public async Task CompleteListAsync(Guid listId, Guid userId)
        {
            var list = await GetOneOrThrowAsync(listId);
            
            ValidateUserAsync(list.UserId, userId);
            list.Complete();
            await _toDoListRepository.PersistAsync(list);
        }

        public async Task StartTaskAsync(Guid listId, Guid taskId, Guid userId)
        {
            var list = await GetOneOrThrowAsync(listId);
            
            ValidateUserAsync(list.UserId, userId);
            var task = list.Tasks.SingleOrDefault(x => x.Id == taskId);
            if (task == null)
            {
                throw new BusinessLogicException("task_not_found");
            }
            task.StartProgress();
            await _toDoListRepository.PersistAsync(list);
        }

        public async Task HoldTaskAsync(Guid listId, Guid taskId, Guid userId)
        {
            var list = await GetOneOrThrowAsync(listId);
            
            ValidateUserAsync(list.UserId, userId);
            var task = list.Tasks.SingleOrDefault(x => x.Id == taskId);
            if (task == null)
            {
                throw new BusinessLogicException("task_not_found");
            }
            task.Hold();
            await _toDoListRepository.PersistAsync(list);
        }

        public async Task CancelTaskAsync(Guid listId, Guid taskId, Guid userId)
        {
            var list = await GetOneOrThrowAsync(listId);
            
            ValidateUserAsync(list.UserId, userId);
            var task = list.Tasks.SingleOrDefault(x => x.Id == taskId);
            if (task == null)
            {
                throw new BusinessLogicException("task_not_found");
            }
            task.Cancel();
            await _toDoListRepository.PersistAsync(list);
        }

        public async Task DeleteTaskAsync(Guid listId, Guid taskId, Guid userId)
        {
            var list = await GetOneOrThrowAsync(listId);
            
            ValidateUserAsync(list.UserId, userId);
            var task = list.Tasks.SingleOrDefault(x => x.Id == taskId);
            if (task == null)
            {
                throw new BusinessLogicException("task_not_found");
            }
            task.MarkAsArchived();
            await _toDoListRepository.PersistAsync(list);
        }

        public async Task CompleteTaskAsync(Guid listId, Guid taskId, Guid userId)
        {
            var list = await GetOneOrThrowAsync(listId);
            
            ValidateUserAsync(list.UserId, userId);
            var task = list.Tasks.SingleOrDefault(x => x.Id == taskId);
            if (task == null)
            {
                throw new BusinessLogicException("task_not_found");
            }
            task.Complete();
            await _toDoListRepository.PersistAsync(list);
        }

        public async Task StartSubTaskAsync(Guid listId, Guid taskId, Guid subTaskId, Guid userId)
        {
            var list = await GetOneOrThrowAsync(listId);
            
            ValidateUserAsync(list.UserId, userId);
            var task = list.Tasks.SingleOrDefault(x => x.Id == taskId);
            if (task == null)
            {
                throw new BusinessLogicException("task_not_found");
            }
            var subTask = task.SubTasks.SingleOrDefault(x => x.Id == subTaskId);
            if (subTask == null)
            {
                throw new BusinessLogicException("task_not_found");
            }
            subTask.StartProgress();
            await _toDoListRepository.PersistAsync(list);
        }

        public async Task HoldSubTaskAsync(Guid listId, Guid taskId, Guid subTaskId, Guid userId)
        {
            var list = await GetOneOrThrowAsync(listId);
            
            ValidateUserAsync(list.UserId, userId);
            var task = list.Tasks.SingleOrDefault(x => x.Id == taskId);
            if (task == null)
            {
                throw new BusinessLogicException("task_not_found");
            }
            var subTask = task.SubTasks.SingleOrDefault(x => x.Id == subTaskId);
            if (subTask == null)
            {
                throw new BusinessLogicException("task_not_found");
            }
            subTask.Hold();
            await _toDoListRepository.PersistAsync(list);
        }

        public async Task CancelSubTaskAsync(Guid listId, Guid taskId, Guid subTaskId, Guid userId)
        {
            var list = await GetOneOrThrowAsync(listId);
            
            ValidateUserAsync(list.UserId, userId);
            var task = list.Tasks.SingleOrDefault(x => x.Id == taskId);
            if (task == null)
            {
                throw new BusinessLogicException("task_not_found");
            }
            var subTask = task.SubTasks.SingleOrDefault(x => x.Id == subTaskId);
            if (subTask == null)
            {
                throw new BusinessLogicException("task_not_found");
            }
            subTask.Cancel();
            await _toDoListRepository.PersistAsync(list);
        }

        public async Task DeleteSubTaskAsync(Guid listId, Guid taskId, Guid subTaskId, Guid userId)
        {
            var list = await GetOneOrThrowAsync(listId);
            
            ValidateUserAsync(list.UserId, userId);
            var task = list.Tasks.SingleOrDefault(x => x.Id == taskId);
            if (task == null)
            {
                throw new BusinessLogicException("task_not_found");
            }
            var subTask = task.SubTasks.SingleOrDefault(x => x.Id == subTaskId);
            if (subTask == null)
            {
                throw new BusinessLogicException("task_not_found");
            }
            subTask.MarkAsArchived();
            await _toDoListRepository.PersistAsync(list);
        }

        public async Task CompleteSubTaskAsync(Guid listId, Guid taskId, Guid subTaskId, Guid userId)
        {
            var list = await GetOneOrThrowAsync(listId);
            
            ValidateUserAsync(list.UserId, userId);
            var task = list.Tasks.SingleOrDefault(x => x.Id == taskId);
            if (task == null)
            {
                throw new BusinessLogicException("task_not_found");
            }
            var subTask = task.SubTasks.SingleOrDefault(x => x.Id == subTaskId);
            if (subTask == null)
            {
                throw new BusinessLogicException("task_not_found");
            }
            subTask.Complete();
            await _toDoListRepository.PersistAsync(list);
        }

        public async Task DeleteAllUserLists(Guid userId)
        {
            var lists = (await _toDoListRepository.GetAllAsync(new ListQueryDataStructure() {UserId = userId})).ToList();

            foreach (var list in lists)
            {
                await DeleteListAsync(list.Id, userId);
            }
        }

        private async Task<ToDoList> GetOneOrThrowAsync(Guid id)
        {
            var toDoList = await _toDoListRepository.GetOneAsync(id);
            if (toDoList == null)
            {
                throw new BusinessLogicException("list_not_found");
            }

            return toDoList;
        }
        
        private void ValidateUserAsync(Guid userId, Guid dataStructureUserId)
        {
            if (userId != dataStructureUserId)
            {
                throw new BusinessLogicException("user_not_permitted");
            }
        }
        
        private async Task<User> GetUserOrThrowAsync(Guid dataStructureUserId)
        {
            var user = await _userRepository.GetAsync(dataStructureUserId);
            if (user == null)
            {
                throw new BusinessLogicException("user_not_found");
            }

            return user;
        }
    }
}