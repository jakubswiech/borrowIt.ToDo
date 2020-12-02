using System;
using BorrowIt.Common.Rabbit.Abstractions;
using BorrowIt.Common.Rabbit.Attributes;

namespace BorrowIt.ToDo.Domain.Model.ToDoList.Events
{
    [RabbitMessage("ToDo")]
    public class ToDoListHourLeftEvent : IMessage
    {
        public Guid ToDoListId { get; private set; }
        public string ToDoListName { get; private set; }
        public Guid UserId { get; private set; }

        public ToDoListHourLeftEvent(Guid toDoListId, string toDoListName, Guid userId)
        {
            ToDoListId = toDoListId;
            ToDoListName = toDoListName;
            UserId = userId;
        }

        public ToDoListHourLeftEvent()
        {
            
        }
    }
}
