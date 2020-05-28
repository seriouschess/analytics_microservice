using analytics.dtos.RequestDtos;

namespace analytics.JsonMessage
{
    public class JsonReportRequestError
    {
        public string message {get;set;}
        public ReportRequest SampleObject{get;set;}
        public JsonReportRequestError(){
            message = "Invalid Response Format. Data requests must be made with a body of the form of the sample object.";
            SampleObject = new ReportRequest();
            SampleObject.domain = "www.example.com";
            SampleObject.day = 00;
            SampleObject.month = 00;
            SampleObject.year = 0000;
        }
    }
}