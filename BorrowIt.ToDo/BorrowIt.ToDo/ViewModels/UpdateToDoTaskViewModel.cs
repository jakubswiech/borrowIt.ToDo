using System;

namespace BorrowIt.ToDo.ViewModels
{
    public class UpdateToDoTaskViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid ListId { get; set; }
    }
}