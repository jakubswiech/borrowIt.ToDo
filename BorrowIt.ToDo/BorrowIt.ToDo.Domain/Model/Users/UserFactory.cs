namespace BorrowIt.ToDo.Domain.Model.Users
{
    public class UserFactory : IUserFactory
    {
        public User Create(UserDataStructure user)
            => new User(user.Id, user.UserName);
    }
}