using System.Collections.Generic;
using analytics.dtos.RequestDtos;
using FluentValidation.Results;

namespace analytics.dtos.ErrorDtos
{
    public class MonthReportRequestError
    {
        public List<string> errors{get;set;}
        public string general {get;set;}
        public MonthReportRequest SampleObject{get;set;}
        public MonthReportRequestError(ValidationResult verdict){
            errors = new List<string>();
            foreach( var error in verdict.Errors){
            errors.Add( $"Error: {error.ToString()}" );
            } 
            general = "Invalid Response Format. Month Report requests must be made with a body of the form of the sample object.";
            SampleObject = new MonthReportRequest();
            SampleObject.domain = "www.example.com";
            SampleObject.month = 00;
            SampleObject.year = 0000;
        }
    }
}