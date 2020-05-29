using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc; //to return http error codes
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

//project specific
using analytics.Classes;
using analytics.dtos;

//db related
using analytics.JsonMessage;
using analytics.Models;
using analytics.Queries;
using analytics.dtos.RequestDtos;
using analytics.dtos.ErrorDtos;
using analytics.dtos.ReportDtos;

namespace analytics.Controllers.ControllerMethods
{
    public class ReportsControllerMethods
    {
        private AnalyticsQueries dbQuery;
        private Authenticator auth;
        private DataFormatter formatter;
        private Reporter reporter;

        private Validator validator;

        public ReportsControllerMethods( AnalyticsQueries _dbQuery ){
            dbQuery = _dbQuery;
            auth = new Authenticator(dbQuery);
            formatter = new DataFormatter();
            reporter = new Reporter();
            validator = new Validator();
        }

        //read all  -- Make all information public?
        public async Task<List<GenericSession>> ReturnSessionsMethod(){
            return await Task.Run(() => 
                dbQuery.getAllSessions()
            );
        }


        //Summary Report Controller Methods

        public async Task<ActionResult<DomainReportSummary>> ReportByMonthMethod(string domain, DateTime month){
            List<GenericSession> reportSessions = await Task.Run(() => dbQuery.getSessionsForDomainByMonth( domain, month));
            return reporter.genericReport(reportSessions);
        }

        public async Task<ActionResult<DomainReportSummary>> ReportByDateMethod(string domain, DateTime date){
            List<GenericSession> reportSessions = await Task.Run(() => dbQuery.getSessionsForDomainByDate( domain, date ));
            return reporter.genericReport(reportSessions);
        }

        public async Task<ActionResult<DomainReportSummary>> ReportBetweenDatesMethod(string domain, DateTime date_one, DateTime date_two){
            
            DateTime min;
            DateTime max;
            if(date_one > date_two){
                max = date_one;
                min = date_two;
            }else{
                min = date_one;
                max = date_two;
            }

            TimeSpan full_days_time = new TimeSpan(23,59,59); //add a day to account 
            max += full_days_time;

            System.Console.WriteLine($"Max Time: {max}");

            List<GenericSession> reportSessions = await Task.Run(() => dbQuery.getSessionsByDomainInDateTimeRange( domain, min, max));
            return reporter.genericReport(reportSessions);
        }



        //Raw Controller Methods
        public async Task<ActionResult<List<GenericSession>>> RawByMonthMethod(string domain, DateTime month){
            List<GenericSession> reportSessions = await Task.Run(() => dbQuery.getSessionsForDomainByMonth( domain, month));
            return reportSessions;
        }

        public async Task<ActionResult<List<GenericSession>>> RawByDateMethod(string domain, DateTime date){
            List<GenericSession> reportSessions = await Task.Run(() => dbQuery.getSessionsForDomainByDate( domain, date ));
            return reportSessions;
        }

        public async Task<ActionResult<List<GenericSession>>> RawBetweenDatesMethod(string domain, DateTime date_one, DateTime date_two){
            
            DateTime min;
            DateTime max;
            if(date_one > date_two){
                max = date_one;
                min = date_two;
            }else{
                min = date_one;
                max = date_two;
            }

            TimeSpan full_days_time = new TimeSpan(23,59,59); //add a day to account 
            max += full_days_time;

            System.Console.WriteLine($"Max Time: {max}");

            List<GenericSession> reportSessions = await Task.Run(() => dbQuery.getSessionsByDomainInDateTimeRange( domain, min, max));
            return reportSessions;
        }

        //Url related Queries

        public async Task<ActionResult<List<GenericSession>>> getAllByDomain(string domain){
            domain = formatter.stripDomain(domain);
            return await Task.Run(() => dbQuery.getSessionsByDomain(domain) );
        }

        public async Task<ActionResult<DomainReportSummary>> getSummaryByDomain(string domain){
            List<GenericSession> AllSessions = await Task.Run(() => dbQuery.getSessionsByDomain( domain ));
            DomainReportSummary summary = reporter.genericReport(AllSessions);
            return summary;
        }

        public async Task<ActionResult<List<GenericSession>>> getAllByUrl(string url){ //maybe validate url regex to ensure . and / exist?
            return await Task.Run(() => dbQuery.getSessionsByUrl(url));
        }

        //DateTime Queries

        public async Task<ActionResult<List<GenericSession>>> getAllBeforeDateTime(DateTime date){
            return await Task.Run(() => dbQuery.getSessionsOnOrBeforeDateTime(date));
        }

        public async Task<ActionResult<List<GenericSession>>> getAllAfterDateTime(DateTime date){
            return await Task.Run(() => dbQuery.getSessionsOnOrAfterDateTime(date));
        }

        public async Task<ActionResult<List<GenericSession>>> getAllByDate(int year, int month, int day){
            DateTime targetDate = new DateTime(year, month, day);
            return await Task.Run(() => dbQuery.getSessionsByDate(targetDate));
        }

        //test method -- For Development only
        public ActionResult<JsonResponse> TestMethod(){
            return new BadRequestResult();
            //return reporter.genericReport(dbQuery.getAllSessions());
        }
    }
}