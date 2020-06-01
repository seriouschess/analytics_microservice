using System;
using System.Collections.Generic;
using analytics.dtos;
using analytics.dtos.ReportDtos;
using analytics.Models;

namespace analytics.Classes
{
    public class Reporter
    {
        private double engagement_hours{get;set;}
        public Reporter(){ }

        private LogDate logNewDay( DateTime current_date, int session_count_for_current_date, double total_engagement_seconds ){
            LogDate log_of_day = new LogDate();
            log_of_day.log_date = current_date;
            log_of_day.log_count = session_count_for_current_date;
            log_of_day.total_engagement_minutes = total_engagement_seconds/60;
            return log_of_day;
        }

        public DomainReportSummary CountByDate( List<GenericSession> data ){
            if(data.Count == 0){ //no entries found edge case
                DomainReportSummary empty_report = new DomainReportSummary();
                empty_report.sessions_by_day = new List<LogDate>();
                empty_report.total_engagement_hours = 0;
                return empty_report;
            }

            DateTime current_date = data[0].created_at;
            List<LogDate> session_count_by_date = new List<LogDate>();
            double daily_engagement_seconds = data[0].time_on_homepage;
            double total_engagement_seconds = daily_engagement_seconds;
            int session_count_for_current_date = 1; //count the first entry


            if(data.Count == 1){ //edge case of only having a single entry
                session_count_by_date.Add(logNewDay(
                                current_date,
                                session_count_for_current_date,
                                daily_engagement_seconds
                            ));

            }else{      //this thread will be used in most cases

                for(var x=1; x < data.Count; x++){ //start from second entry
                    //System.Console.WriteLine($"Daily Engagement Seconds: {daily_engagement_seconds}");
                    //System.Console.WriteLine($"Total Engagement Seconds: {total_engagement_seconds}");
                    total_engagement_seconds += data[x].time_on_homepage;
                    daily_engagement_seconds += data[x].time_on_homepage;
                    
                    // System.Console.WriteLine($"Iterator Value: {data[x].time_on_homepage}");
                    // System.Console.WriteLine($"Daily Engagement Seconds: {daily_engagement_seconds}");
                    // System.Console.WriteLine($"Total Engagement Seconds: {total_engagement_seconds}");

                    if( data[ x ].created_at.Date == current_date.Date ){ //same day
                        
                        session_count_for_current_date += 1;
                        if( x == data.Count-1 ){ //end of list, log remaining results
                            session_count_by_date.Add(logNewDay(
                                current_date,
                                session_count_for_current_date,
                                daily_engagement_seconds
                            ));
                        }
                        
                    }else{ //new day, log previous day and continue

                        session_count_by_date.Add(logNewDay(
                                current_date,
                                session_count_for_current_date,
                                daily_engagement_seconds - data[x].time_on_homepage //current day not counted... yeah
                            ));
                        current_date = data[x].created_at.Date;
                        session_count_for_current_date = 1; //count the day
                        // System.Console.WriteLine($"Changed Hours: {total_engagement_seconds}");
                        daily_engagement_seconds = data[x].time_on_homepage; //reset seconds
                    }
                }

            }
            DomainReportSummary report = new DomainReportSummary();
            report.sessions_by_day =  session_count_by_date;
            report.total_engagement_hours = total_engagement_seconds/3600;
            return report;
        }

        public DomainReportSummary genericReport( List<GenericSession> data){
            DomainReportSummary report = new DomainReportSummary();
            report = CountByDate(data);
            return report;
        }
    }
}