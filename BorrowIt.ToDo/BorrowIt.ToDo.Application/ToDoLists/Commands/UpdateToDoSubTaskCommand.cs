using System;
using BorrowIt.Common.Infrastructure.Abstraction.Commands;

namespace BorrowIt.ToDo.Application.ToDoLists.Commands
{
    public class UpdateToDoSubTaskCommand : ICommand
    {
        public Guid Id { get; }
        public string Name { get; }
        public string Description { get; }
        public Guid? UserId { get; }
        public Guid ListId { get; }
        public Guid TaskId { get; }

        public UpdateToDoSubTaskCommand(Guid id, string name, string description, Guid? userId, Guid listId, Guid taskId)
        {
            ListId = listId;
            TaskId = taskId;
            Id = id;
            Name = name;
            Description = description;
            UserId = userId;
        }
    }
}