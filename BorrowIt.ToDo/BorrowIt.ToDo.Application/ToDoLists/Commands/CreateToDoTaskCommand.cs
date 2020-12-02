using System;
using BorrowIt.Common.Infrastructure.Abstraction.Commands;

namespace BorrowIt.ToDo.Application.ToDoLists.Commands
{
    public class CreateToDoTaskCommand : ICommand
    {
        public string Name { get; }
        public string Description { get; }
        public Guid? UserId { get; }
        public Guid Id { get; }
        public Guid ListId { get; }

        public CreateToDoTaskCommand(Guid listId, string name, string description, Guid? userId)
        {
            Name = name;
            Description = description;
            UserId = userId;
            Id = Guid.NewGuid();
            ListId = listId;
        }
    }
}