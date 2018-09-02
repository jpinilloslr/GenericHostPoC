using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MyApplication.Models;
using MyApplication.Services;
using MyApplication.Services.External;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace GenericHostPoC
{
    public class HostedService : IHostedService
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<HostedService> _logger;
        private readonly IApplicationService _applicationService;

        public HostedService(
            IConfiguration configuration,
            ILogger<HostedService> logger,
            IApplicationService applicationService)
        {
            _logger = logger;
            _configuration = configuration;
            _applicationService = applicationService;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            try
            {
                var input = _configuration.Get<ApplicationInput>();
                await _applicationService.DoWork(input);
            }
            catch (Exception exception)
            {
                _logger.LogCritical(exception, "An error occurred.");
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
