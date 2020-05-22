using System;

namespace analytics.dtos
{
    public class LogDate
    {
        public DateTime log_date {get;set;}
        public int log_count {get;set;}
        public double total_engagement_minutes {get;set;}
    }
}