using AutoMapper;
using BorrowIt.Common.Mongo.Repositories;
using BorrowIt.ToDo.Domain.Model.ToDoList;
using MongoDB.Driver;

namespace BorrowIt.ToDo.Infrastructure.Entities.ToDoTasks
{
    public class ToDoTaskMongoRepository : GenericMongoRepository<ToDoTask, ToDoTaskEntity>, IToDoTaskMongoRepository
    {
        public ToDoTaskMongoRepository(IMongoDatabase database, IMapper mapper) : base(database, mapper)
        {
        }
    }
}