using System.Collections.Generic;

namespace analytics.dtos
{
    public class ReportDto
    {
        public List<LogDate> sessions_by_day{get;set;}
        public double total_engagement_hours{get;set;}
    }
}