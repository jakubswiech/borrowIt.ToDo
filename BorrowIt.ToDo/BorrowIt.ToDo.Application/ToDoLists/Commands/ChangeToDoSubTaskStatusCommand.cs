using System;
using BorrowIt.Common.Infrastructure.Abstraction.Commands;

namespace BorrowIt.ToDo.Application.ToDoLists.Commands
{
    public class ChangeToDoSubTaskStatusCommand : ICommand
    {
        public Guid ListId { get; set; }
        public Guid TaskId { get; set; }
        public Guid SubTaskId { get; set; }
        public int Status { get; set; }
    }
}