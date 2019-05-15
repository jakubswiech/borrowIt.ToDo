using System;
using BorrowIt.Common.Mongo.Models;
using MongoDB.Bson.Serialization.Attributes;

namespace BorrowIt.ToDo.Infrastructure.Entities.Users
{
    public class UserEntity : IMongoEntity
    {
        [BsonId]
        public Guid Id { get; set; }
        public string UserName { get; set; }
    }
}