using System;
using analytics.dtos.RequestDtos;
using FluentValidation;

namespace analytics.Validators
{
    public class DateRangeReportValidator: AbstractValidator<DateRangeReportRequest>
    {
        public string domain {get;set;}
        public int year {get;set;}
        public int month {get;set;}
        public int day {get;set;}
        public DateRangeReportValidator()
        {
            RuleFor(r => r.domain).Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty().WithMessage("Domain missing")
                .Length(5, 1000).WithMessage("URL not between 5 and 1000 characters") //1000 character url is pretty excessive right?
                .Must(ValidateDomainContents).WithMessage("Domain must be a valid format including '.'");

            RuleFor(r => r.date_one).NotEmpty().WithMessage("date_one missing"); //date one

            RuleFor(r => r.date_one.year).Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty().WithMessage("Year not included")
                .InclusiveBetween(2020, DateTime.Now.Year).WithMessage("Year not between 2020 and now"); //Module made in 2020

            RuleFor(r => r.date_one.month).Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty().WithMessage("Month not included")
                .InclusiveBetween(1, 12).WithMessage("Month not an integer between 1 and 12");

            RuleFor(r => r.date_one.day).Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty().WithMessage("Day not included")
                .InclusiveBetween(1, 32).WithMessage("Day not between 1 and 32");

            RuleFor(r => r.date_two).NotEmpty().WithMessage("date_one missing"); //date two

            RuleFor(r => r.date_two.year).Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty().WithMessage("Year not included")
                .InclusiveBetween(2020, DateTime.Now.Year).WithMessage("Year not between 2020 and now"); //Module made in 2020

            RuleFor(r => r.date_two.month).Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty().WithMessage("Month not included")
                .InclusiveBetween(1, 12).WithMessage("Month not an integer between 1 and 12");

            RuleFor(r => r.date_two.day).Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty().WithMessage("Day not included")
                .InclusiveBetween(1, 32).WithMessage("Day not between 1 and 32");

        }

        protected bool ValidateDomainContents(string domain){
            if(domain.Contains(".")){
                return true;
            }else{
                return false;
            }
        }

    }
}