using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using BorrowIt.Common.Rabbit.Abstractions;
using BorrowIt.ToDo.Domain.Model.Users;
using BorrowIt.ToDo.Infrastructure.Inboud.Messages;

namespace BorrowIt.ToDo.Application.Users.Handlers
{
    public class UserChangedEventHandler : IMessageHandler<UserChangedEvent>
    {
        private readonly IUsersService _usersService;
        private readonly IMapper _mapper;

        public UserChangedEventHandler(IUsersService usersService, IMapper mapper)
        {
            _usersService = usersService;
            _mapper = mapper;
        }
       

        public async Task HandleMessageAsync(UserChangedEvent message, CancellationToken token)
        {
            var dataStructure = _mapper.Map<UserDataStructure>(message);
            if (await _usersService.UserExists(dataStructure.Id))
            {
                await _usersService.UpdateUserAsync(dataStructure);
            }
            else
            {
                await _usersService.AddUserAsync(dataStructure);
            }
        }
    }
}