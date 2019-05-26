using System;
using System.Threading.Tasks;
using BorrowIt.Common.Domain;
using BorrowIt.Common.Exceptions;
using BorrowIt.Common.Extensions;

namespace BorrowIt.ToDo.Domain.Model.ToDoList
{
    public class ToDoSubTask : DomainModel
    {
        public Guid TaskId { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public ToDoListStatus Status { get; private set; }
        public DateTime CreateDate { get; private set; }
        public DateTime ModifyDate { get; private set; }

        public ToDoSubTask(Guid id, Guid taskId, string name, string description)
        {
            Id = id;
            TaskId = taskId;
            SetName(name);
            SetDescription(description);
            Status = ToDoListStatus.Created;
            CreateDate = DateTime.UtcNow;
            ModifyDate = CreateDate;
        }

        private ToDoSubTask()
        {
            
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
            Status = ToDoListStatus.Archived;
        }

        public void Complete()
        {
            ValidateStatus();
            Status = ToDoListStatus.Done;
        }

        public void Cancel()
        {
            ValidateStatus();
            Status = ToDoListStatus.Cancelled;
        }

        public void Hold()
        {
            ValidateStatus();
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
        
    }
}