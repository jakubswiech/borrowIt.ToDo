using System;

namespace BorrowIt.ToDo.ViewModels
{
    public class ChangeToDoSubTaskStatusViewModel
    {
        public Guid ListId { get; set; }
        public Guid TaskId { get; set; }
        public Guid SubTaskId { get; set; }
        public int Status { get; set; }
    }
}