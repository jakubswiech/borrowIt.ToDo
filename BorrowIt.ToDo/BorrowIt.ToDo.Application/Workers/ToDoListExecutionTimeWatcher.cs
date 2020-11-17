using System;
using System.Threading;
using System.Threading.Tasks;
using BorrowIt.ToDo.Application.ToDoLists;
using Microsoft.Extensions.Hosting;

namespace BorrowIt.ToDo.Application.Workers
{
    public class ToDoListExecutionTimeWatcher : IHostedService, IDisposable
    {
        private readonly IToDoListsService _service;
        private Timer _timer;

        public ToDoListExecutionTimeWatcher(IToDoListsService service)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
        }
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            _timer = new Timer(async (x) => await CheckToDoListsExecutionTimeAsync(x), null, TimeSpan.Zero, TimeSpan.FromMinutes(1));
        }

        private async Task CheckToDoListsExecutionTimeAsync(object state)
        {
            await _service.NotifyAllEndingToDoLists();
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer.Dispose();
        }
    }
}
