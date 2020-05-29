using System.ComponentModel.DataAnnotations;

namespace analytics.dtos.RequestDtos
{
    public class DateRangeReportRequest
    {
        [Required]
        public string domain{get;set;}
        [Required]
        public date date_one{get;set;}
        [Required]
        public date date_two{get;set;}
        
    }
}