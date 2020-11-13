using System;
using BorrowIt.Common.Mongo.Attributes;
using BorrowIt.Common.Mongo.Models;

namespace BorrowIt.ToDo.Infrastructure.Entities.ToDoLists
{
    [MongoEntity("ToDoLists")]
    public class ToDoListEntity : MongoEntity
    {
        public Guid UserId { get; set; }
        public string Name { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime ModifyDate { get; set; }
        public int Status { get; set; }
    }
}