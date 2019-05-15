namespace BorrowIt.ToDo.Domain.Model.Users
{
    public interface IUserFactory
    {
        User Create(UserDataStructure user);
    }
}