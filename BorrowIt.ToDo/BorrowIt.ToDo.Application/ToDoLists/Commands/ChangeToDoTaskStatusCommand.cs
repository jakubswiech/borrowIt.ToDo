using System;
using BorrowIt.Common.Infrastructure.Abstraction.Commands;

namespace BorrowIt.ToDo.Application.ToDoLists.Commands
{
    public class ChangeToDoTaskStatusCommand : ICommand
    {
        public Guid ListId { get; }
        public Guid TaskId { get; }
        public int Status { get; }
        public Guid? UserId { get; }

        public ChangeToDoTaskStatusCommand(Guid listId, Guid taskId, int status, Guid? userId)
        {
            ListId = listId;
            TaskId = taskId;
            Status = status;
            UserId = userId;
        }
    }
}