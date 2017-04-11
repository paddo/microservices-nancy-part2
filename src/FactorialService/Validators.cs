using FluentValidation;
using MicroservicesDemo.FactorialService.Models;

namespace MicroservicesDemo.FactorialService
{
    public class FactorialRequestValidator : AbstractValidator<FactorialRequest>
    {
        public FactorialRequestValidator()
        {
            RuleFor(request => request.Number).InclusiveBetween(1,12);
        }
    }
}