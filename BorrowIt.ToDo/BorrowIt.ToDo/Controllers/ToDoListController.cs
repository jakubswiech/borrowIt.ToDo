using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BorrowIt.Common.Infrastructure.Abstraction;
using BorrowIt.ToDo.Application.ToDoLists.Commands;
using BorrowIt.ToDo.Application.ToDoLists.DTOs;
using BorrowIt.ToDo.Application.ToDoLists.Queries;
using BorrowIt.ToDo.ViewModels;
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
        public async Task<IActionResult> Post([FromBody] CreateToDoListViewModel viewModel)
        {
            var userId = new Guid(User.Identity.Name);
            var command = new CreateToDoListCommand(viewModel.Name, viewModel.FinishUntilDate, userId);
            await CommandDispatcher.DispatchAsync(command);

            return Ok();
        }
        
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] UpdateToDoListViewModel viewModel)
        {
            var userId = new Guid(User.Identity.Name);
            var command = new UpdateToDoListCommand(viewModel.Id, viewModel.Name, userId);
            
            await CommandDispatcher.DispatchAsync(command);

            return Ok();
        }
        [HttpPut("ChangeStatus")]
        public async Task<IActionResult> Put([FromBody] ChangeToDoListStatusViewModel viewModel)
        {
            var userId = new Guid(User.Identity.Name);
            var command = new ChangeToDoListStatusCommand(viewModel.ListId, viewModel.Status, userId);
            
            await CommandDispatcher.DispatchAsync(command);

            return Ok();
        }

        [HttpPost("Tasks")]
        public async Task<IActionResult> Post([FromBody] CreateToDoTaskViewModel viewModel)
        {
            var userId = new Guid(User.Identity.Name);
            var command = new CreateToDoTaskCommand(viewModel.ListId, viewModel.Name, viewModel.Description, userId);

            await CommandDispatcher.DispatchAsync(command);

            return Ok();
        }
        [HttpPut("Tasks")]
        public async Task<IActionResult> Put([FromBody] UpdateToDoTaskViewModel viewModel)
        {
            var userId = new Guid(User.Identity.Name);
            var command = new UpdateToDoTaskCommand(viewModel.Id, viewModel.ListId, viewModel.Name, viewModel.Description, userId);
            
            await CommandDispatcher.DispatchAsync(command);

            return Ok();
        }

        [HttpPut("Tasks/ChangeStatus")]
        public async Task<IActionResult> Put([FromBody] ChangeToDoTaskStatusViewModel viewModel)
        {
            var userId = new Guid(User.Identity.Name);
            var command = new ChangeToDoTaskStatusCommand(viewModel.ListId, viewModel.TaskId, viewModel.Status, userId);
            
            await CommandDispatcher.DispatchAsync(command);

            return Ok();
        }
        [HttpPost("SubTasks")]
        public async Task<IActionResult> Post([FromBody] CreateToDoSubTaskViewModel viewModel)
        {
            var userId = new Guid(User.Identity.Name);
            var command = new CreateToDoSubTaskCommand(viewModel.ListId, viewModel.TaskId, viewModel.Name, viewModel.Description, userId);

            await CommandDispatcher.DispatchAsync(command);

            return Ok();
        }

        [HttpPut("SubTasks")]
        public async Task<IActionResult> Put([FromBody] UpdateToDoSubTaskViewModel viewModel)
        {
            var userId = new Guid(User.Identity.Name);
            var command = new UpdateToDoSubTaskCommand(viewModel.Id, viewModel.Name, viewModel.Description, userId, viewModel.ListId, viewModel.TaskId);
            
            await CommandDispatcher.DispatchAsync(command);

            return Ok();
        }
        [HttpPut("SubTasks/ChangeStatus")]
        public async Task<IActionResult> Put([FromBody] ChangeToDoSubTaskStatusViewModel viewModel)
        {
            var userId = new Guid(User.Identity.Name);
            var command = new ChangeToDoSubTaskStatusCommand(viewModel.ListId, viewModel.TaskId, viewModel.SubTaskId, viewModel.Status, userId);
            
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