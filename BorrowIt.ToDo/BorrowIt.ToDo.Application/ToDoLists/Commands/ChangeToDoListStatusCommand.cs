using System;
using BorrowIt.Common.Infrastructure.Abstraction.Commands;

namespace BorrowIt.ToDo.Application.ToDoLists.Commands
{
    public class ChangeToDoListStatusCommand : ICommand
    {
        public Guid ListId { get; }
        public int Status { get; }
        public Guid UserId { get; }

        public ChangeToDoListStatusCommand(Guid listId, int status, Guid userId)
        {
            ListId = listId;
            Status = status;
            UserId = userId;
        }
    }
}