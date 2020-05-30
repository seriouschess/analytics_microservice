using System.ComponentModel.DataAnnotations;

namespace analytics.dtos.RequestDtos
{
    public class DateReportRequest
    {
        [Required]
        public string domain {get;set;}
        [Required]
        public int year {get;set;}
        [Required]
        public int month {get;set;}
        [Required]
        public int day {get;set;}
    }
}