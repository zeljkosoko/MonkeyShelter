using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MonkeyShelter.Core.Interfaces;

namespace MonkeyShelter.Services.BackgroundJobs
{
    public class VetCheckReminderService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<VetCheckReminderService> _logger;

        public VetCheckReminderService(IServiceProvider serviceProvider, ILogger<VetCheckReminderService> logger)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using (var scope = _serviceProvider.CreateScope())
                {
                    var vetCheckRepo = scope.ServiceProvider.GetRequiredService<IVetCheckRepository>();

                    var overdueMonkeys = await vetCheckRepo.GetMonkeysMissingCheckAsync();

                    if (overdueMonkeys.Any())
                    {
                        _logger.LogWarning("⚠️ {Count} monkey(ies) are overdue for a vet check!", overdueMonkeys.Count());
                    }
                    else
                    {
                        _logger.LogInformation("✅ All monkeys are up to date with vet checks.");
                    }
                }

                // Wait 24h..
                await Task.Delay(TimeSpan.FromHours(24), stoppingToken);
            }
        }
    }
}
