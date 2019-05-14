using BorrowIt.Common.Domain.Repositories;
using BorrowIt.ToDo.Domain.Model.ToDoList;

namespace BorrowIt.ToDo.Infrastructure.ToDoSubTasks
{
    public interface IToDoSubTaskMongoRepository : IGenericRepository<ToDoSubTask, ToDoSubTaskEntity>, IRepository
    {
        
    }
}