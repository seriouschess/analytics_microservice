using System.Collections.Generic;
using analytics.dtos.RequestDtos;
using FluentValidation.Results;

namespace analytics.dtos.ErrorDtos
{
    public class DateRangeReportRequestError
    {
        public List<string> errors {get;set;}
        public string general {get;set;}
        public DateRangeReportRequest SampleObject{get;set;}
        
        public DateRangeReportRequestError(ValidationResult verdict){
            errors = new List<string>();
            foreach( var error in verdict.Errors){
               errors.Add( $"Error: {error.ToString()}" );
            } 
            general = "Invalid Response Format. Date range report requests must be made with a body of the form of the sample object.";
            SampleObject = new DateRangeReportRequest();
            SampleObject.domain = "www.example.com";
            SampleObject.date_one = new date();
            SampleObject.date_two = new date();
            SampleObject.date_one.month = 00;
            SampleObject.date_one.year = 0000;
            SampleObject.date_one.day = 00;
            SampleObject.date_two.month = 00;
            SampleObject.date_two.year = 0000;
            SampleObject.date_two.day = 00;
        }
    }
}