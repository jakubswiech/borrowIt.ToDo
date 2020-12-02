using System;

namespace BorrowIt.ToDo.ViewModels
{
    public class UpdateToDoListViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime FinishUntilDate { get; set; }
    }
}
