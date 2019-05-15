using AutoMapper;
using BorrowIt.Common.Mongo.Repositories;
using BorrowIt.ToDo.Domain.Model.ToDoList;
using MongoDB.Driver;

namespace BorrowIt.ToDo.Infrastructure.Entities.ToDoSubTasks
{
    public class ToDoSubTaskMongoRepository : GenericMongoRepository<ToDoSubTask, ToDoSubTaskEntity>, IToDoSubTaskMongoRepository
    {
        public ToDoSubTaskMongoRepository(IMongoDatabase database, IMapper mapper) : base(database, mapper)
        {
        }
    }
}