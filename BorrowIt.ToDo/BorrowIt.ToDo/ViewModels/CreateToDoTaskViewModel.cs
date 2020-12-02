using System;

namespace BorrowIt.ToDo.ViewModels
{
    public class CreateToDoTaskViewModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid? Id { get; set; }
        public Guid ListId { get; set; }
    }
}