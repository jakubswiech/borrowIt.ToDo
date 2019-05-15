using System;
using System.Collections.Generic;
using System.Linq;
using BorrowIt.Common.Domain;
using BorrowIt.Common.Exceptions;
using BorrowIt.Common.Extensions;
using BorrowIt.ToDo.Domain.Model.ToDoList.DataStructures;

namespace BorrowIt.ToDo.Domain.Model.ToDoList
{
    public class ToDoTask : DomainModel
    {
        public Guid ListId { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public ToDoListStatus Status { get; private set; }
        public DateTime CreateDate { get; private set; }
        public DateTime ModifyDate { get; private set; }
        private List<ToDoSubTask> _subTasks = new List<ToDoSubTask>();
        public IEnumerable<ToDoSubTask> SubTasks
        {
            get => _subTasks.ToList();
            private set => _subTasks = value.ToList();
        }

        public ToDoTask(Guid id, Guid listId, string name, string description)
        {
            Id = id;
            ListId = listId;
            SetName(name);
            SetName(description);
            Status = ToDoListStatus.Created;
            CreateDate = DateTime.UtcNow;
            ModifyDate = CreateDate;
        }
        
        public void Update(string name, string description)
        {
            ValidateStatus();
            ModifyDate = DateTime.UtcNow;
            SetName(name);
            SetDescription(description);
        }

        public void MarkAsArchived()
        {
            foreach (var subTask in SubTasks)
            {
                subTask.MarkAsArchived();
            }
            Status = ToDoListStatus.Archived;
        }

        public void Complete()
        {
            ValidateStatus();
            if (!SubTasks.Any() || SubTasks.Any(x => x.Status != ToDoListStatus.Done))
            {
                throw new BusinessLogicException("subtasks_not_done");
            }
            
            Status = ToDoListStatus.Done;
        }

        public void Cancel()
        {
            ValidateStatus();
            foreach (var subTask in SubTasks)
            {
                subTask.Cancel();
            }
            Status = ToDoListStatus.Cancelled;
        }

        public void Hold()
        {
            ValidateStatus();
            foreach (var subTask in SubTasks)
            {
                subTask.Hold();
            }
            Status = ToDoListStatus.OnHold;
        }

        public void StartProgress()
        {
            ValidateStatus();
            Status = ToDoListStatus.InProgress;
        }

        public void AddSubTask(SubTaskDataStructure dataStructure)
        {
            if (Status != ToDoListStatus.InProgress)
            {
                throw new BusinessLogicException("task_should_be_started");
            }
            var subTask = new ToDoSubTask(dataStructure.Id, Id, dataStructure.Name, dataStructure.Description);
            
            _subTasks.Add(subTask);
        }

        public void UpdateSubTask(SubTaskDataStructure dataStructure)
        {
            if (Status != ToDoListStatus.InProgress)
            {
                throw new BusinessLogicException("task_should_be_started");
            }
            var subTask = SubTasks.SingleOrDefault(x => x.Id == dataStructure.Id);
            if (subTask == null)
            {
                throw new BusinessLogicException("subtask_not_found");
            }
            
            subTask.Update(dataStructure.Name, dataStructure.Description);
        }

        private void ValidateStatus()
        {
            if (Status == ToDoListStatus.Done || Status == ToDoListStatus.Archived ||
                Status == ToDoListStatus.Cancelled)
            {
                throw new BusinessLogicException("invalid_task_status");
            }
        }

        private void SetName(string name)
        {
            name.ValidateNullOrEmptyString(nameof(name));
            Name = name;
        }

        private void SetDescription(string description)
        {
            description.ValidateNullOrEmptyString(nameof(description));
            Description = description;
        }

        public void InsertSubTasks(IEnumerable<ToDoSubTask> subTasks)
        {
            _subTasks.AddRange(subTasks);
        }
    }
}