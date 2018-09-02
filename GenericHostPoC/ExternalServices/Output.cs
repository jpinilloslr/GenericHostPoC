using Microsoft.Extensions.Logging;
using MyApplication.Services.External;

namespace GenericHostPoC.ExternalServices
{
    public class Output : IOutput
    {
        private readonly ILogger _logger;

        public Output(ILogger<Output> logger)
        {
            _logger = logger;
        }

        public void Information(string message) => _logger.LogInformation(message);

        public void Warning(string message) => _logger.LogWarning(message);

        public void Error(string message) => _logger.LogError(message);

        public void Critical(string message) => _logger.LogCritical(message);
    }
}
