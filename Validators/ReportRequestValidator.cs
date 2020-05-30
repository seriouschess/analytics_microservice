using analytics.dtos.RequestDtos;
using FluentValidation;

namespace analytics.Validators
{
    public class ReportRequestValidator: AbstractValidator<DateReportRequest>
    {
         public ReportRequestValidator()
        {
            RuleFor(p => p.year).NotEmpty();
        }
    }
}