using System;
using BorrowIt.Common.Infrastructure.Abstraction.Commands;

namespace BorrowIt.ToDo.Application.ToDoLists.Commands
{
    public class UpdateToDoListCommand : ICommand
    {
        public Guid Id { get; }
        public string Name { get; }
        public Guid UserId { get; }

        public UpdateToDoListCommand(Guid id, string name, Guid userId)
        {
            Id = id;
            Name = name;
            UserId = userId;
        }
    }
}