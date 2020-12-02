using System;
using BorrowIt.Common.Infrastructure.Abstraction.Commands;

namespace BorrowIt.ToDo.Application.ToDoLists.Commands
{
    public class CreateToDoSubTaskCommand : ICommand
    {
        public string Name { get; }
        public string Description { get; }
        public Guid UserId { get; }
        public Guid ListId { get; }
        public Guid Id { get; }
        public Guid TaskId { get; }

        public CreateToDoSubTaskCommand(Guid listId, Guid taskId, string name, string description, Guid userId)
        {
            Name = name;
            Description = description;
            UserId = userId;
            ListId = listId;
            Id = Guid.NewGuid();
            TaskId = taskId;
        }
    }
}