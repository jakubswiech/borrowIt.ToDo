using System;

namespace BorrowIt.ToDo.ViewModels
{
    public class CreateToDoListViewModel
    {
        public string Name { get; set; }
        public DateTime FinishUntilDate { get; set; }
    }
}