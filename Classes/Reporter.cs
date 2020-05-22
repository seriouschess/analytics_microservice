using System;
using System.Collections.Generic;
using analytics.dtos;
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

        public ReportDto CountByDate( List<GenericSession> data ){
               DateTime current_date = data[0].created_at;
               List<LogDate> session_count_by_date = new List<LogDate>();
               double daily_engagement_seconds = data[0].time_on_homepage;
               double total_engagement_hours = daily_engagement_seconds/3600;
               int session_count_for_current_date = 1; //count the first entry

            if(data.Count == 1){ //edge case of only having a single entry
                session_count_by_date.Add(logNewDay(
                                current_date,
                                session_count_for_current_date,
                                daily_engagement_seconds
                            ));
            }else{ 
                //most every case of multiple entries
                for(var x=1; x < data.Count; x++){ //start from second entry
                    daily_engagement_seconds += data[x].time_on_homepage;
                    total_engagement_hours += data[x].time_on_homepage/3600;
                     System.Console.WriteLine($"Engagement Seconds Total: {total_engagement_hours}");
                      System.Console.WriteLine($"Daily Engagement Seconds: {daily_engagement_seconds}");
                    if( data[x].created_at.Date == current_date.Date ){ //same day
                        session_count_for_current_date += 1;
                        if(x == data.Count-1){ //end of list, log remaining results
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
                                daily_engagement_seconds
                            ));
                        current_date = data[x].created_at.Date;
                        session_count_for_current_date = 1; //count the day
                        daily_engagement_seconds = data[x].time_on_homepage; //reset seconds
                        total_engagement_hours += data[x].time_on_homepage/3600;
                    }
                }

            }
            ReportDto report = new ReportDto();
            report.sessions_by_day =  session_count_by_date;
            report.total_engagement_hours = total_engagement_hours;
            return report;
        }

        public ReportDto genericReport( List<GenericSession> data){
            ReportDto report = new ReportDto();
            report = CountByDate(data);
            return report;
        }
    }
}