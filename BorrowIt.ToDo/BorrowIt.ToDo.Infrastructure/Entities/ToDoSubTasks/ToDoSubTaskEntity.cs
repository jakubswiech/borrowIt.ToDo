using System;
using BorrowIt.Common.Mongo.Models;
using MongoDB.Bson.Serialization.Attributes;

namespace BorrowIt.ToDo.Infrastructure.Entities.ToDoSubTasks
{
    public class ToDoSubTaskEntity : IMongoEntity
    {
        [BsonId]
        public Guid Id { get; set; }
        public Guid TaskId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Status { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime ModifyDate { get; set; }
        
    }
}