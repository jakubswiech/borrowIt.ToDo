using AutoMapper;
using BorrowIt.Common.Mongo.Models;
using BorrowIt.Common.Mongo.Repositories;
using BorrowIt.ToDo.Domain.Model.ToDoList;
using MongoDB.Driver;

namespace BorrowIt.ToDo.Infrastructure.Entities.ToDoTasks
{
    public class ToDoTaskMongoRepository : GenericMongoRepository<ToDoTask, ToDoTaskEntity>, IToDoTaskMongoRepository
    {
        public ToDoTaskMongoRepository(IMongoClient mongoClient, IMapper mapper, MongoDbSettings mongoDbSettings) : base(mongoClient, mapper, mongoDbSettings)
        {
        }
    }
}