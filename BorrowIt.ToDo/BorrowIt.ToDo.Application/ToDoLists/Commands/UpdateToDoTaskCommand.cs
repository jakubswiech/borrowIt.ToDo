using System;
using BorrowIt.Common.Infrastructure.Abstraction.Commands;

namespace BorrowIt.ToDo.Application.ToDoLists.Commands
{
    public class UpdateToDoTaskCommand : ICommand
    {
        public Guid Id { get; }
        public string Name { get; }
        public string Description { get; }
        public Guid? UserId { get; }
        public Guid ListId { get; }

        public UpdateToDoTaskCommand(Guid id, Guid listId, string name, string description, Guid? userId)
        {
            Id = id;
            Name = name;
            Description = description;
            UserId = userId;
            ListId = listId;
        }
    }
}