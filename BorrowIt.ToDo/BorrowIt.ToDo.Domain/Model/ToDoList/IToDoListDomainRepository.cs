using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using BorrowIt.Common.Domain.Repositories;
using BorrowIt.ToDo.Domain.Model.ToDoList.DataStructures;

namespace BorrowIt.ToDo.Domain.Model.ToDoList
{
    public interface IToDoListDomainRepository : IRepository
    {
        Task<ToDoList> PersistAsync(ToDoList toDoList);
        Task<IEnumerable<ToDoList>> GetAllAsync(ListQueryDataStructure queryDataStructure = null);
        Task<ToDoList> GetOneAsync(Guid id);
    }
}