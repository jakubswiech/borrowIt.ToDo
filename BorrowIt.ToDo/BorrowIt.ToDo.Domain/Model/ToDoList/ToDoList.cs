using System;
using System.Collections.Generic;
using System.Linq;
using BorrowIt.Common.Domain;
using BorrowIt.Common.Exceptions;
using BorrowIt.Common.Extensions;
using BorrowIt.ToDo.Domain.Model.ToDoList.DataStructures;

namespace BorrowIt.ToDo.Domain.Model.ToDoList
{
    public class ToDoList : DomainModel
    {
        public Guid UserId { get; private set; }
        public string Name { get; private set; }
        public DateTime CreateDate { get; private set; }
        public DateTime ModifyDate { get; private set; }
        public DateTime FinishUntilDate { get; private set; }
        public ToDoListStatus Status { get; private set; }

        private List<ToDoTask> _tasks = new List<ToDoTask>();

        public IEnumerable<ToDoTask> Tasks
        {
            get => _tasks;
            private set => _tasks = value.ToList();
        }
        
        public ToDoList(Guid id, Guid userId, string name, DateTime finishUntilDate)
        {
            Id = id;
            UserId = userId;
            SetName(name);
            CreateDate = DateTime.UtcNow;
            ModifyDate = CreateDate;
            Status = ToDoListStatus.Created;
            FinishUntilDate = finishUntilDate;
        }

        public void Update(string name, DateTime finishUntilDate)
        {
            SetName(name);
            ModifyDate = DateTime.UtcNow;
            FinishUntilDate = finishUntilDate;
        }
        
        public void MarkAsArchived()
        {
            foreach (var task in Tasks)
            {
                task.MarkAsArchived();
            }
            Status = ToDoListStatus.Archived;
        }

        public void Complete()
        {
            ValidateStatus(Status != ToDoListStatus.OnHold);
            if (Tasks.Any(x => x.Status != ToDoListStatus.Done))
            {
                throw new BusinessLogicException("tasks_not_done");
            }
            
            Status = ToDoListStatus.Done;
        }

        public void Cancel()
        {
            ValidateStatus();
            foreach (var task in Tasks)
            {
                task.Cancel();
            }
            Status = ToDoListStatus.Cancelled;
        }

        public void Hold()
        {
            ValidateStatus();
            foreach (var task in Tasks)
            {
                task.Hold();
            }
            Status = ToDoListStatus.OnHold;
        }

        public void StartProgress()
        {
            if (Status != ToDoListStatus.Created && Status != ToDoListStatus.OnHold)
            {
                throw new BusinessLogicException("invalid_list_status");
            }
            Status = ToDoListStatus.InProgress;
        }

        public void AddTask(TaskDataStructure dataStructure)
        {
            var task = new ToDoTask(dataStructure.Id, Id, dataStructure.Name, dataStructure.Description);
            _tasks.Add(task);
        }

        public void UpdateTask(TaskDataStructure dataStructure)
        {
            var task = Tasks.SingleOrDefault(x => x.Id == dataStructure.Id);
            if (task == null)
            {
                throw new BusinessLogicException("task_not_found");
            }
            task.Update(dataStructure.Name, dataStructure.Description);
        }

        private void SetName(string name)
        {
            name.ValidateNullOrEmptyString(nameof(name));
            Name = name;
        }
        
        public void InsertTasks(IEnumerable<ToDoTask> tasks)
        {
            _tasks.AddRange(tasks);
        }
        
        private void ValidateStatus(bool predicate = true)
        {
            if (Status == ToDoListStatus.Done || Status == ToDoListStatus.Archived ||
                Status == ToDoListStatus.Cancelled || !predicate)
            {
                throw new BusinessLogicException("invalid_list_status");
            }
        }

    }
}