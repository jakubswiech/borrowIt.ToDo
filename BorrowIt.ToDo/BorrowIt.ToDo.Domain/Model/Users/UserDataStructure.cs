using System;

namespace BorrowIt.ToDo.Domain.Model.Users
{
    public class UserDataStructure
    {
        public Guid Id { get; private set; }
        public string UserName { get; private set; }

        public UserDataStructure(Guid id, string userName)
        {
            Id = id;
            UserName = userName;
        }
    }
}