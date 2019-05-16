using System;
using BorrowIt.Common.Infrastructure.Abstraction.Commands;

namespace BorrowIt.ToDo.Application.ToDoLists.Commands
{
    public class CreateToDoTaskCommand : ICommand
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid? UserId { get; set; }
        public Guid? Id { get; set; }
        public Guid ListId { get; set; }
    }
}