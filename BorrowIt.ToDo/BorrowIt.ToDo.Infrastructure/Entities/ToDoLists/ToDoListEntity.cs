using System;
using BorrowIt.Common.Mongo.Models;

namespace BorrowIt.ToDo.Infrastructure.Entities.ToDoLists
{
    public class ToDoListEntity : IMongoEntity
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string Name { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime ModifyDate { get; set; }
        public int Status { get; set; }
    }
}