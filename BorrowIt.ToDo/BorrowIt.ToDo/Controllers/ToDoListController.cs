using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BorrowIt.Common.Infrastructure.Abstraction;
using BorrowIt.ToDo.Application.ToDoLists.Commands;
using BorrowIt.ToDo.Application.ToDoLists.DTOs;
using BorrowIt.ToDo.Application.ToDoLists.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BorrowIt.ToDo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ToDoListController : BaseController
    {
        public ToDoListController(ICommandDispatcher commandDispatcher, IMapper mapper, IQueryDispatcher queryDispatcher) : base(commandDispatcher, mapper, queryDispatcher)
        {
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateToDoListCommand command)
        {
            var userId = User.Identity.Name;
            command.UserId = new Guid(userId);
            await CommandDispatcher.DispatchAsync(command);

            return Ok();
        }
        
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] UpdateToDoListCommand command)
        {
            var userId = User.Identity.Name;
            command.UserId = new Guid(userId);
            
            await CommandDispatcher.DispatchAsync(command);

            return Ok();
        }
        [HttpPut("ChangeStatus")]
        public async Task<IActionResult> Put([FromBody] ChangeToDoListStatusCommand command)
        {
            var userId = User.Identity.Name;
            command.UserId = new Guid(userId);
            
            await CommandDispatcher.DispatchAsync(command);

            return Ok();
        }
        
        [HttpPost("Tasks")]
        public async Task<IActionResult> Post([FromBody] CreateToDoTaskCommand command)
        {
            var userId = User.Identity.Name;
            command.UserId = new Guid(userId);
            
            command.Id = command.Id ?? Guid.NewGuid();
            await CommandDispatcher.DispatchAsync(command);

            return Ok();
        }
        [HttpPut("Tasks")]
        public async Task<IActionResult> Put([FromBody] UpdateToDoTaskCommand command)
        {
            var userId = User.Identity.Name;
            command.UserId = new Guid(userId);
            
            await CommandDispatcher.DispatchAsync(command);

            return Ok();
        }

        [HttpPut("Tasks/ChangeStatus")]
        public async Task<IActionResult> Put([FromBody] ChangeToDoTaskStatusCommand command)
        {
            var userId = User.Identity.Name;
            command.UserId = new Guid(userId);
            
            await CommandDispatcher.DispatchAsync(command);

            return Ok();
        }
        [HttpPost("SubTasks")]
        public async Task<IActionResult> Post([FromBody] CreateToDoSubTaskCommand command)
        {
            var userId = User.Identity.Name;
            command.UserId = new Guid(userId);
            
            command.Id = command.Id ?? Guid.NewGuid();
            await CommandDispatcher.DispatchAsync(command);

            return Ok();
        }

        [HttpPut("SubTasks")]
        public async Task<IActionResult> Put([FromBody] UpdateToDoSubTaskCommand command)
        {
            var userId = User.Identity.Name;
            command.UserId = new Guid(userId);
            
            await CommandDispatcher.DispatchAsync(command);

            return Ok();
        }
        [HttpPut("SubTasks/ChangeStatus")]
        public async Task<IActionResult> Put([FromBody] ChangeToDoSubTaskStatusCommand command)
        {
            var userId = User.Identity.Name;
            command.UserId = new Guid(userId);
            
            await CommandDispatcher.DispatchAsync(command);

            return Ok();
        }

        [Authorize]
        [HttpPost("GetLists")]
        public async Task<IActionResult> Post([FromBody] ToDoListQuery query)
        {
            var userId = User.Identity.Name;
            query.UserId = new Guid(userId);
            
            
            var result = await QueryDispatcher.DispatchQueryAsync<ToDoListResultDto, ToDoListQuery>(query);
            return Ok(result);
        }
    }
}