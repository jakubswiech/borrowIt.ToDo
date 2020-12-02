using System;

namespace BorrowIt.ToDo.ViewModels
{
    public class ChangeToDoListStatusViewModel
    {
        public Guid ListId { get; set; }
        public int Status { get; set; }
    }
}