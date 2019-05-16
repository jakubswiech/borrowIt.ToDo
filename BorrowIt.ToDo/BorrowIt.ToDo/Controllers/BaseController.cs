using AutoMapper;
using BorrowIt.Common.Infrastructure.Abstraction;
using Microsoft.AspNetCore.Mvc;

namespace BorrowIt.ToDo.Controllers
{
    public abstract class BaseController : ControllerBase
    {
        protected readonly ICommandDispatcher CommandDispatcher;
        protected readonly IQueryDispatcher QueryDispatcher;
        protected readonly IMapper Mapper;

        protected BaseController(ICommandDispatcher commandDispatcher, IMapper mapper, IQueryDispatcher queryDispatcher)
        {
            CommandDispatcher = commandDispatcher;
            Mapper = mapper;
            QueryDispatcher = queryDispatcher;
        }
    }
}