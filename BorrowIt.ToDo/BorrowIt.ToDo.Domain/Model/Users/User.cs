using System;
using BorrowIt.Common.Domain;
using BorrowIt.Common.Extensions;

namespace BorrowIt.ToDo.Domain.Model.Users
{
    public class User : DomainModel
    {
        public string UserName { get; private set; }

        public User(Guid id, string userName)
        {
            Id = id;
            SetUserName(userName);
            
        }

        private User()
        {

        }

        public void Update(string userName)
        {
            SetUserName(userName);
        }

        private void SetUserName(string userName)
        {
            userName.ValidateNullOrEmptyString();

            UserName = userName;
        }
        
    }
}