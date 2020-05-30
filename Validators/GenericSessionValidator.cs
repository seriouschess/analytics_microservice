using analytics.Models;
using FluentValidation;

namespace analytics.Validators
{
    public class GenericSessionValidator: AbstractValidator<GenericSession>
    {
        public GenericSessionValidator()
        {
            RuleFor(p => p.token).NotEmpty();
        }
    }
}