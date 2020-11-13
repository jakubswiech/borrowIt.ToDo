using System;
using System.Threading;
using System.Threading.Tasks;
using BorrowIt.Common.Infrastructure.Abstraction;
using BorrowIt.Common.Rabbit.Abstractions;
using BorrowIt.ToDo.Application.Users.Commands;
using BorrowIt.ToDo.Infrastructure.Inboud.Messages;


namespace BorrowIt.ToDo.Application.Users.Handlers.Messages
{
    public class UserChangedEventHandler : IMessageHandler<UserChangedEvent>
    {
        private readonly ICommandDispatcher _commandDispatcher;

        public UserChangedEventHandler(ICommandDispatcher commandDispatcher)
        {
            _commandDispatcher = commandDispatcher ?? throw new ArgumentNullException(nameof(commandDispatcher));
        }

        public async Task HandleMessageAsync(UserChangedEvent message, CancellationToken token)
        {
            await _commandDispatcher.DispatchAsync(new SynchronizeUserCommand(message.Id, message.UserName));
        }
    }
}