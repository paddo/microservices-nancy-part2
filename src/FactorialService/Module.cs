using Microsoft.Extensions.Logging;
using Nancy;
using MicroservicesDemo.FactorialService.Models;

namespace MicroservicesDemo.FactorialService
{
    public class Module : NancyModule
    {
        private ILogger<Module> _logger;

        public Module(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<Module>();

            Get("/factorial/{number:int}", p => GetFactorial(new FactorialRequest { Number = p.number }));
        }

        FactorialResponse GetFactorial(FactorialRequest request)
        {
            var result = new FactorialResponse { Success = true, Factorial = Factorial(request.Number) };
            _logger.LogInformation($"Generated Factorial for {request.Number}: {result.Factorial}");

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