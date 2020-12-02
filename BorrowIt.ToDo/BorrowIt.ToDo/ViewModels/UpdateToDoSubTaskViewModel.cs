using System;

namespace BorrowIt.ToDo.ViewModels
{
    public class UpdateToDoSubTaskViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid ListId { get; set; }
        public Guid TaskId { get; set; }
    }
}