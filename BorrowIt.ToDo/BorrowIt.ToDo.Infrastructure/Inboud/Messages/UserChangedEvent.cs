using System;
using BorrowIt.Common.Rabbit.Abstractions;
using BorrowIt.Common.Rabbit.Attributes;

namespace BorrowIt.ToDo.Infrastructure.Inboud.Messages
{
    [RabbitMessage("Auth")]
    public class UserChangedEvent : IMessage 
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
    }
}