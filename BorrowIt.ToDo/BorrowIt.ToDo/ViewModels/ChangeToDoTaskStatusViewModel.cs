using System;

namespace BorrowIt.ToDo.ViewModels
{
    public class ChangeToDoTaskStatusViewModel
    {
        public Guid ListId { get; set; }
        public Guid TaskId { get; set; }
        public int Status { get; set; }
    }
}