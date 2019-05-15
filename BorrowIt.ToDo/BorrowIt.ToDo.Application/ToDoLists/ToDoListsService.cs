using System;
using System.Linq;
using System.Threading.Tasks;
using BorrowIt.Common.Exceptions;
using BorrowIt.ToDo.Domain.Model.ToDoList;
using BorrowIt.ToDo.Domain.Model.ToDoList.DataStructures;
using BorrowIt.ToDo.Domain.Model.ToDoList.Factories;

namespace BorrowIt.ToDo.Application.ToDoLists
{
    public class ToDoListsService : IToDoListsService
    {
        private readonly IToDoListDomainRepository _toDoListRepository;
        private readonly IListFactory _toDoListFactory;

        public ToDoListsService(IToDoListDomainRepository toDoListRepository, IListFactory toDoListFactory)
        {
            _toDoListRepository = toDoListRepository;
            _toDoListFactory = toDoListFactory;
        }
        
        public async Task AddListAsync(ListDataStructure dataStructure)
        {
            var toDoList = _toDoListFactory.Create(dataStructure);

            await _toDoListRepository.PersistAsync(toDoList);
        }

        public async Task UpdateListAsync(ListDataStructure dataStructure)
        {
            var toDoList = await GetOneOrThrowAsync(dataStructure.Id?? new Guid());
            toDoList.Update(dataStructure.Name);

            await _toDoListRepository.PersistAsync(toDoList);
        }

        public async Task AddTaskAsync(TaskDataStructure dataStructure, Guid listId)
        {
            var toDoList = await GetOneOrThrowAsync(listId);
            
            toDoList.AddTask(dataStructure);

            await _toDoListRepository.PersistAsync(toDoList);
        }

        public async Task UpdateTaskAsync(TaskDataStructure dataStructure, Guid listId)
        {
            var toDoList = await GetOneOrThrowAsync(listId);
            
            toDoList.UpdateTask(dataStructure);

            await _toDoListRepository.PersistAsync(toDoList);
        }

        public async Task AddSubTaskAsync(SubTaskDataStructure dataStructure, Guid listId, Guid taskId)
        {
            var toDoList = await GetOneOrThrowAsync(listId);
            var task = toDoList.Tasks.SingleOrDefault(x => x.Id == taskId);
            if (task == null)
            {
                throw new BusinessLogicException("task_not_found");
            }
            
            task.AddSubTask(dataStructure);
            await _toDoListRepository.PersistAsync(toDoList);
        }

        public async Task UpdateSubTaskAsync(SubTaskDataStructure dataStructure, Guid listId, Guid taskId)
        {
            var toDoList = await GetOneOrThrowAsync(listId);
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

        private async Task<ToDoList> GetOneOrThrowAsync(Guid id)
        {
            var toDoList = await _toDoListRepository.GetOneAsync(id);
            if (toDoList == null)
            {
                throw new BusinessLogicException("list_not_found");
            }

            return toDoList;
        }
    }
}