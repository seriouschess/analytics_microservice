using System.Collections.Generic;
using analytics.dtos.RequestDtos;
using FluentValidation.Results;

namespace analytics.dtos.ErrorDtos
{
    public class DateReportRequestError
    {
        public List<string> errors {get;set;}
        public string general {get;set;}
       
        public DateReportRequest SampleObject{get;set;}
        public DateReportRequestError(ValidationResult verdict){
            errors = new List<string>();
            foreach( var error in verdict.Errors){
               errors.Add( $"Error: {error.ToString()}" );
            } 
            general = "Invalid Response Format. Date report requests must be made with a body of the form of the sample object.";
            SampleObject = new DateReportRequest();
            SampleObject.domain = "www.example.com";
            SampleObject.day = 00;
            SampleObject.month = 00;
            SampleObject.year = 0000;
        }
    }
}