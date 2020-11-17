using System;
using BorrowIt.Common.Infrastructure.Abstraction.Commands;

namespace BorrowIt.ToDo.Application.ToDoLists.Commands
{
    public class CreateToDoListCommand : ICommand
    {
        public string Name { get; set; }
        public Guid UserId { get; set; }
        public DateTime FinishUntilDate { get; set; }
    }
}