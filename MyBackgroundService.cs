using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;
using W1.Data;




namespace W1
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.Logging;
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Xml;
    using W1.Data;
    using W1.Models;


    public class MyBackgroundService : BackgroundService
    {
        private readonly ILogger<MyBackgroundService> _logger;
        private readonly IServiceProvider _serviceProvider;

        public MyBackgroundService(ILogger<MyBackgroundService> logger, IServiceProvider serviceProvider)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("MyBackgroundService is starting.");

            using var timer = new PeriodicTimer(TimeSpan.FromSeconds(10));

            while (!stoppingToken.IsCancellationRequested && await timer.WaitForNextTickAsync(stoppingToken))
            {
                _logger.LogInformation("MyBackgroundService is performing work.");
                await DoWork(stoppingToken);
            }

            _logger.LogInformation("MyBackgroundService is stopping.");
        }

        private async Task DoWork(CancellationToken stoppingToken)
        {
            try
            {
                // Create a new scope for each unit of work
                using IServiceScope scope = _serviceProvider.CreateScope();
                var dbContext = scope.ServiceProvider.GetRequiredService<PlacidDBContext>();

                // Perform a database operation
                await dbContext.Members.ToListAsync();


                //  await dbContext..SaveChangesAsync(stoppingToken);

                _logger.LogInformation($"finished");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while doing background work.");
            }
        }
    }



    public class MyHostedService : IHostedService
    {
        private readonly ILogger<MyHostedService> _logger;

        public MyHostedService(ILogger<MyHostedService> logger)
        {
            _logger = logger;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("MyHostedService starting.");
            // Place your startup logic here
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("MyHostedService stopping.");
            // Place your cleanup logic here
            return Task.CompletedTask;
        }
    }
}
