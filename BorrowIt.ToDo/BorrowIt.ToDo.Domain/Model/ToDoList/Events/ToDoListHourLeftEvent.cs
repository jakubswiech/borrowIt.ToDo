using System;
using BorrowIt.Common.Rabbit.Abstractions;

namespace BorrowIt.ToDo.Domain.Model.ToDoList.Events
{
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
