using System;
using BorrowIt.Common.Infrastructure.Abstraction.Commands;

namespace BorrowIt.ToDo.Application.ToDoLists.Commands
{
    public class ChangeToDoSubTaskStatusCommand : ICommand
    {
        public Guid ListId { get; }
        public Guid TaskId { get; }
        public Guid SubTaskId { get; }
        public int Status { get; }
        public Guid? UserId { get; }

        public ChangeToDoSubTaskStatusCommand(Guid listId, Guid taskId, Guid subTaskId, int status, Guid? userId)
        {
            ListId = listId;
            TaskId = taskId;
            SubTaskId = subTaskId;
            Status = status;
            UserId = userId;
        }
    }
}