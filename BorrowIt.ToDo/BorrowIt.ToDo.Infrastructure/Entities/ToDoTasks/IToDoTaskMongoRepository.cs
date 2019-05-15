using BorrowIt.Common.Domain.Repositories;
using BorrowIt.ToDo.Domain.Model.ToDoList;

namespace BorrowIt.ToDo.Infrastructure.Entities.ToDoTasks
{
    public interface IToDoTaskMongoRepository : IGenericRepository<ToDoTask, ToDoTaskEntity>, IRepository
    {
        
    }
}