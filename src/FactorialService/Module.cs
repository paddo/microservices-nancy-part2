using Microsoft.Extensions.Logging;
using Nancy;
using MicroservicesDemo.FactorialService.Models;
using Nancy.Validation;
using LoggingService.Models;
using System.Threading.Tasks;

namespace MicroservicesDemo.FactorialService
{
    public class Module : NancyModule
    {
        private ILogger<Module> _logger;
        private ServiceClient<LogRequest, LogResponse> _logServiceClient;

        public Module(ILoggerFactory loggerFactory, ServiceClient<LogRequest, LogResponse> logServiceClient)
        {
            _logger = loggerFactory.CreateLogger<Module>();
            _logServiceClient = logServiceClient;

            Get("/factorial/{number:int}", p => GetFactorial(new FactorialRequest { Number = p.number }));
        }

        async Task<FactorialResponse> GetFactorial(FactorialRequest request)
        {
            var validationResult = this.Validate(request);
            if (!validationResult.IsValid)
            {
                await _logServiceClient.Post(new LogRequest { Message = $"Failed to generate factorial for {request.Number}" });
                return new FactorialResponse { Success = false, Errors = validationResult.FormattedErrors };
            }
            
            var result = new FactorialResponse { Success = true, Factorial = Factorial(request.Number) };
            await _logServiceClient.Post(new LogRequest { Message = $"Generated Factorial for {request.Number}: {result.Factorial}"});

            return result;
        }

        int Factorial(int i)
        {
            if (i <= 1)
                return 1;
            return i * Factorial(i - 1);
        }
    }
}