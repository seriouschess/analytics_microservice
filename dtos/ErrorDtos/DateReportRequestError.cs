using analytics.dtos.RequestDtos;

namespace analytics.dtos.ErrorDtos
{
    public class DateReportRequestError
    {
        public string message {get;set;}
        public DateReportRequest SampleObject{get;set;}
        public DateReportRequestError(){
            message = "Invalid Response Format. Date report requests must be made with a body of the form of the sample object.";
            SampleObject = new DateReportRequest();
            SampleObject.domain = "www.example.com";
            SampleObject.day = 00;
            SampleObject.month = 00;
            SampleObject.year = 0000;
        }
    }
}