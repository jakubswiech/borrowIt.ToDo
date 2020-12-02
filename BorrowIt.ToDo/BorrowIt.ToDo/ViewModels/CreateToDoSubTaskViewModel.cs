using System;

namespace BorrowIt.ToDo.ViewModels
{
    public class CreateToDoSubTaskViewModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid ListId { get; set; }
        public Guid? Id { get; set; }
        public Guid TaskId { get; set; }
    }
}