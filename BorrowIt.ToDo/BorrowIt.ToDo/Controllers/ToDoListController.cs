using System;
using System.Threading.Tasks;
using AutoMapper;
using BorrowIt.Common.Infrastructure.Abstraction;
using BorrowIt.ToDo.Application.ToDoLists.Commands;
using BorrowIt.ToDo.Application.ToDoLists.DTOs;
using BorrowIt.ToDo.Application.ToDoLists.Queries;
using Microsoft.AspNetCore.Mvc;

namespace BorrowIt.ToDo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToDoListController : BaseController
    {
        public ToDoListController(ICommandDispatcher commandDispatcher, IMapper mapper, IQueryDispatcher queryDispatcher) : base(commandDispatcher, mapper, queryDispatcher)
        {
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateToDoListCommand command)
        {
            await CommandDispatcher.DispatchAsync(command);

            return Ok();
        }
        
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] UpdateToDoListCommand command)
        {
            await CommandDispatcher.DispatchAsync(command);

            return Ok();
        }
        
        [HttpPost("Tasks")]
        public async Task<IActionResult> Post([FromBody] CreateToDoTaskCommand command)
        {
            command.Id = command.Id ?? Guid.NewGuid();
            await CommandDispatcher.DispatchAsync(command);

            return Ok();
        }
        [HttpPut("Tasks")]
        public async Task<IActionResult> Put([FromBody] UpdateToDoTaskCommand command)
        {
            await CommandDispatcher.DispatchAsync(command);

            return Ok();
        }
        [HttpPost("SubTasks")]
        public async Task<IActionResult> Post([FromBody] CreateToDoSubTaskCommand command)
        {
            await CommandDispatcher.DispatchAsync(command);

            return Ok();
        }

        [HttpPut("SubTasks")]
        public async Task<IActionResult> Put([FromBody] UpdateToDoSubTaskCommand command)
        {
            await CommandDispatcher.DispatchAsync(command);

            return Ok();
        }

        [HttpPost("GetLists")]
        public async Task<IActionResult> Post([FromBody] ToDoListQuery query)
        {
            var result = await QueryDispatcher.DispatchQueryAsync<ToDoListResultDto, ToDoListQuery>(query);
            return Ok(result);
        }
    }
}