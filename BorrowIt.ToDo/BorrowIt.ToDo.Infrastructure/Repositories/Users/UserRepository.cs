using AutoMapper;
using BorrowIt.Common.Mongo.Models;
using BorrowIt.Common.Mongo.Repositories;
using BorrowIt.ToDo.Domain.Model.Users;
using BorrowIt.ToDo.Infrastructure.Entities.Users;
using MongoDB.Driver;

namespace BorrowIt.ToDo.Infrastructure.Repositories.Users
{
    public class UserRepository : GenericMongoRepository<User, UserEntity>, IUserRepository
    {
        public UserRepository(IMongoClient mongoClient, IMapper mapper, MongoDbSettings mongoDbSettings) : base(mongoClient, mapper, mongoDbSettings)
        {
        }
    }
}