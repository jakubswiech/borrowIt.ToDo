using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BorrowIt.ToDo.Domain.Model.ToDoList;
using BorrowIt.ToDo.Infrastructure.Entities.ToDoLists;
using BorrowIt.ToDo.Infrastructure.Entities.ToDoSubTasks;
using BorrowIt.ToDo.Infrastructure.Entities.ToDoTasks;

namespace BorrowIt.ToDo.Infrastructure.Repositories.ToDoLists
{
    public class ToDoListRepository : IToDoListDomainRepository
    {
        private readonly IToDoListMongoRepository _toDoListMongoRepository;
        private readonly IToDoTaskMongoRepository _toDoTaskMongoRepository;
        private readonly IToDoSubTaskMongoRepository _toDoSubTaskMongoRepository;

        public ToDoListRepository(IToDoListMongoRepository toDoListMongoRepository, 
            IToDoTaskMongoRepository toDoTaskMongoRepository, IToDoSubTaskMongoRepository toDoSubTaskMongoRepository)
        {
            _toDoListMongoRepository = toDoListMongoRepository;
            _toDoTaskMongoRepository = toDoTaskMongoRepository;
            _toDoSubTaskMongoRepository = toDoSubTaskMongoRepository;
        }
        
        public async Task<ToDoList> PersistAsync(ToDoList toDoList)
        {
            var listInDb = await _toDoListMongoRepository.GetAsync(toDoList.Id);
            if (listInDb == null)
            {
                await _toDoListMongoRepository.CreateAsync(toDoList);
                await PersistTasksAsync(toDoList.Tasks);
            }
            else
            {
                await _toDoListMongoRepository.UpdateAsync(toDoList);
                await PersistTasksAsync(toDoList.Tasks);
            }

            return toDoList;
        }

        public async Task<IEnumerable<ToDoList>> GetAllAsync()
        {
            var lists = (await _toDoListMongoRepository.GetAllAsync()).ToList();

            foreach (var list in lists)
            {
                var tasks = (await _toDoTaskMongoRepository.GetWithExpressionAsync(x => x.ListId == list.Id)).ToList();
                list.InsertTasks(tasks);

                foreach (var task in tasks)
                {
                    var subTasks = (await _toDoSubTaskMongoRepository.GetWithExpressionAsync(x => x.TaskId == task.Id))
                        .ToList();
                    task.InsertSubTasks(subTasks);
                }
            }

            return lists;
        }

        public async Task<ToDoList> GetOneAsync(Guid id)
        {
            var list = await _toDoListMongoRepository.GetAsync(id);
            
            var tasks = (await _toDoTaskMongoRepository.GetWithExpressionAsync(x => x.ListId == list.Id)).ToList();
            list.InsertTasks(tasks);

            foreach (var task in tasks)
            {
                var subTasks = (await _toDoSubTaskMongoRepository.GetWithExpressionAsync(x => x.TaskId == task.Id))
                    .ToList();
                task.InsertSubTasks(subTasks);
            }

            return list;
        }
        
        private async Task PersistTasksAsync(IEnumerable<ToDoTask> tasks)
        {
            foreach (var task in tasks)
            {
                var taskInDb = _toDoTaskMongoRepository.GetAsync(task.Id);
                if (taskInDb == null)
                {
                    await _toDoTaskMongoRepository.CreateAsync(task);
                    await PersistSubTasksAsync(task.SubTasks);

                }
                else
                {
                    await _toDoTaskMongoRepository.UpdateAsync(task);
                    await PersistSubTasksAsync(task.SubTasks);
                }
            }
        }

        private async Task PersistSubTasksAsync(IEnumerable<ToDoSubTask> taskSubTasks)
        {
            foreach (var subTask in taskSubTasks)
            {
                var taskInDb = _toDoSubTaskMongoRepository.GetAsync(subTask.Id);
                if (taskInDb == null)
                {
                    await _toDoSubTaskMongoRepository.CreateAsync(subTask);
                }
                else
                {
                    await _toDoSubTaskMongoRepository.UpdateAsync(subTask);
                }
            }
        }
    }
}