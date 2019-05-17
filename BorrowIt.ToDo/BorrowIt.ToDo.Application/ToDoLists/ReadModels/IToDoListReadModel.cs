using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Xml.Linq;
using AutoMapper;
using BorrowIt.ToDo.Application.ToDoLists.DTOs;
using BorrowIt.ToDo.Application.ToDoLists.Queries;
using BorrowIt.ToDo.Domain.Model.ToDoList;

namespace BorrowIt.ToDo.Application.ToDoLists.ReadModels
{
    public interface IToDoListReadModel
    {
        Task<ToDoListResultDto> GetToDoListResultDto(ToDoListQuery query);
    }
    
    public class ToDoListReadModel : IToDoListReadModel
    {
        private readonly IToDoListDomainRepository _toDoListDomainRepository;
        private readonly IMapper _mapper;

        public ToDoListReadModel(IToDoListDomainRepository toDoListDomainRepository, IMapper mapper)
        {
            _toDoListDomainRepository = toDoListDomainRepository;
            _mapper = mapper;
        }

        public async Task<ToDoListResultDto> GetToDoListResultDto(ToDoListQuery query)
        {
            var predicate = ApplyWhereStatement(query);
            var results = await _toDoListDomainRepository.GetAllAsync(predicate);

            var dtos = _mapper.Map<IEnumerable<ToDoListDto>>(results);

            return new ToDoListResultDto() {Items = dtos.ToList()};
        }

        private Expression<Func<ToDoList, bool>> ApplyWhereStatement(ToDoListQuery query)
        {
            Expression<Func<ToDoList, bool>> predicate = x => String.IsNullOrWhiteSpace(x.Name);
            if (query.Id.HasValue)
            {
                Expression<Func<ToDoList, bool>> expression = x => x.Id == query.Id.Value;
                var newExp = Expression.And(predicate.Body, expression.Body);
                predicate = Expression.Lambda<Func<ToDoList, bool>>(newExp, predicate.Parameters[0]);
            }

            if (query.Status.HasValue)
            {
                Expression<Func<ToDoList, bool>> expression = x => (int)x.Status == query.Status.Value;
                var newExp = Expression.And(predicate.Body, expression.Body);
                predicate = Expression.Lambda<Func<ToDoList, bool>>(newExp, predicate.Parameters[0]);
            }

            if (query.UserId.HasValue)
            {
                Expression<Func<ToDoList, bool>> expression = x => x.UserId == query.UserId.Value;
                var newExp = Expression.And(predicate.Body, expression.Body);
                predicate = Expression.Lambda<Func<ToDoList, bool>>(newExp, predicate.Parameters[0]);
            }

            if (!string.IsNullOrWhiteSpace(query.Name))
            {
                    Expression<Func<ToDoList, bool>> expression = x => x.Name.Contains(query.Name);
                    var newExp = Expression.And(predicate.Body, expression.Body);
                    predicate = Expression.Lambda<Func<ToDoList, bool>>(newExp, predicate.Parameters[0]);
            }
            
            return predicate;
        }
    }
}