using System;
using BorrowIt.Common.Infrastructure.Abstraction.Commands;

namespace BorrowIt.ToDo.Application.ToDoLists.Commands
{
    public class ChangeToDoListStatusCommand : ICommand
    {
        public Guid ListId { get; set; }
        public int Status { get; set; }
        public Guid UserId { get; set; }
    }
}