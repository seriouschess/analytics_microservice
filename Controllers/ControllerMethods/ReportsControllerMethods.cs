using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc; //to return http error codes
using System.Threading.Tasks;

//project specific
using analytics.Classes;
using analytics.dtos;

//db related
using analytics.JsonMessage;
using analytics.Models;
using analytics.Queries;

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

        //Url related Queries

        public async Task<ActionResult<List<GenericSession>>> getAllByDomain(string domain){
            domain = formatter.stripDomain(domain);
            return await Task.Run(() => dbQuery.getSessionsByDomain(domain) );
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

        public async Task<ActionResult<List<GenericSession>>> getAllInDateTimeRange(DateTime min_date, DateTime max_date){
            return await Task.Run(() => dbQuery.getSessionsInDateTimeRange(min_date, max_date));
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