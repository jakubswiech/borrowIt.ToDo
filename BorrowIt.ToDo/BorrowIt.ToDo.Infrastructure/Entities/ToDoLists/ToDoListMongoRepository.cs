using AutoMapper;
using BorrowIt.Common.Mongo.Repositories;
using BorrowIt.ToDo.Domain.Model.ToDoList;
using MongoDB.Driver;

namespace BorrowIt.ToDo.Infrastructure.Entities.ToDoLists
{
    public class ToDoListMongoRepository : GenericMongoRepository<ToDoList, ToDoListEntity>, IToDoListMongoRepository
    {
        public ToDoListMongoRepository(IMongoDatabase database, IMapper mapper) : base(database, mapper)
        {
        }
    }
}