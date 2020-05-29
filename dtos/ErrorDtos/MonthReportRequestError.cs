using analytics.dtos.RequestDtos;

namespace analytics.dtos.ErrorDtos
{
    public class MonthReportRequestError
    {
        public class DateReportRequestError
        {
            public string message {get;set;}
            public MonthReportRequest SampleObject{get;set;}
            public DateReportRequestError(){
                message = "Invalid Response Format. Month Report requests must be made with a body of the form of the sample object.";
                SampleObject = new MonthReportRequest();
                SampleObject.domain = "www.example.com";
                SampleObject.month = 00;
                SampleObject.year = 0000;
            }
        }
    }
}