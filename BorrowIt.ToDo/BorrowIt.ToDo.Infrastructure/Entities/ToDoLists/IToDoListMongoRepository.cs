using BorrowIt.Common.Domain.Repositories;
using BorrowIt.ToDo.Domain.Model.ToDoList;

namespace BorrowIt.ToDo.Infrastructure.Entities.ToDoLists
{
    public interface IToDoListMongoRepository : IGenericRepository<ToDoList, ToDoListEntity>, IRepository
    {
        
    }
}