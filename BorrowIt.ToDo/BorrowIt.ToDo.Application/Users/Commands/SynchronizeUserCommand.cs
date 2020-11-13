using System;
using BorrowIt.Common.Infrastructure.Abstraction.Commands;

namespace BorrowIt.ToDo.Application.Users.Commands
{
    public class SynchronizeUserCommand : ICommand
    {
        public Guid Id { get; }
        public string UserName { get; }

        public SynchronizeUserCommand(Guid id, string userName)
        {
            Id = id;
            UserName = userName;
        }
    }
}
