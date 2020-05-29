using System.ComponentModel.DataAnnotations;

namespace analytics.dtos.RequestDtos
{
    public class MonthReportRequest
    {
        [Required]
        public string domain{get;set;}
        [Required]
        public int year{get;set;}
        [Required]
        public int month{get;set;}
    }
}