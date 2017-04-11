using Microsoft.Extensions.Logging;
using Nancy;
using Nancy.ModelBinding;
using LoggingService.Models;
using Nancy.Validation;

namespace LoggingService
{
    public class Module : NancyModule
    {
        private ILogger<Module> _logger;

        public Module(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<Module>();

            Post("/log", p => PostLogRequest());
        }

        LogResponse PostLogRequest()
        {
            var request = this.Bind<LogRequest>();
            var validationResult = this.Validate(request);

            if (!validationResult.IsValid)
            {
                _logger.LogInformation($"Failed to log message '{request.Message}'");
                return new LogResponse { Success = false, Errors = validationResult.FormattedErrors };
            }
            
            _logger.LogInformation($"Logging Message: '{request.Message}'");

            return new LogResponse { Success = true };
        }

    }
}