using System;
using BorrowIt.Common.Infrastructure.Abstraction.Commands;

namespace BorrowIt.ToDo.Application.ToDoLists.Commands
{
    public class CreateToDoListCommand : ICommand
    {
        public Guid Id { get; }
        public string Name { get; }
        public Guid UserId { get; }
        public DateTime FinishUntilDate { get; }

        public CreateToDoListCommand(string name, DateTime finishUntilDate, Guid userId)
        {
            Name = name;
            UserId = userId;
            FinishUntilDate = finishUntilDate;
            Id = Guid.NewGuid();
        }
    }
}