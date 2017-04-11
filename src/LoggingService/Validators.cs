using FluentValidation;
using LoggingService.Models;

namespace LoggingService
{
    public class FactorialRequestValidator : AbstractValidator<LogRequest>
    {
        public FactorialRequestValidator()
        {
            RuleFor(request => request.Message).NotEmpty();
        }
    }
}