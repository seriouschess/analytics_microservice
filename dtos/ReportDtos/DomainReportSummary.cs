using System;
using System.Collections.Generic;

namespace analytics.dtos.ReportDtos
{
    public class DomainReportSummary
    {
        public List<LogDate> sessions_by_day {get;set;}
        public double total_engagement_hours {get;set;}
    }
}