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
    public class AnalyticsControllerMethods
    {
        private AnalyticsQueries dbQuery;
        private Authenticator auth;
        private DataFormatter formatter;
        private Reporter reporter;

        public AnalyticsControllerMethods( AnalyticsQueries _dbQuery ){
            dbQuery = _dbQuery;
            auth = new Authenticator(dbQuery);
            formatter = new DataFormatter();
            reporter = new Reporter();
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
            return await Task.Run(() => dbQuery.getSessionsBeforeDateTime(date));
        }

        public async Task<ActionResult<List<GenericSession>>> getAllAfterDateTime(DateTime date){
            return await Task.Run(() => dbQuery.getSessionsAfterDateTime(date));
        }

        public async Task<ActionResult<List<GenericSession>>> getAllInDateTimeRange(DateTime min_date, DateTime max_date){
            return await Task.Run(() => dbQuery.getSessionsInDateTimeRange(min_date, max_date));
        }

        public async Task<ActionResult<List<GenericSession>>> getAllByDate(int year, int month, int day){
            DateTime minDate = new DateTime(year, month, day);
            return await Task.Run(() => dbQuery.getSessionsByDate(minDate));
        }

        //CRUD actions
        public async Task<ActionResult<GenericSession>> genUserSessionMethod(GenericSession _NewSession){
            if(auth.authenticateGeneralUse(_NewSession.token)){
                GenericSession NewSession = new GenericSession();
                NewSession.time_on_homepage = _NewSession.time_on_homepage;
                NewSession.url = _NewSession.url;
                NewSession.token = auth.GenerateToken();
                await Task.Run(() => dbQuery.addSession(NewSession));
                return NewSession;
            }else{                //authentication fail
                return new GenericSession(); 
            } 
        }

        public async Task<ActionResult<JsonResponse>> UpdateSessionMethod( GenericSession CurrentSession ){
            if(auth.validateToken(CurrentSession.session_id, CurrentSession.token)){
                await Task.Run(() => dbQuery.updateSession( CurrentSession ));
                return new JsonResponse("Session Updated");
            }else{  //auth fail
                return new JsonResponse("There's nothing to fear but fear itself.");
            }

        }

        public async Task<ActionResult<JsonResponse>> DeleteAllMethod(){
            await Task.Run( () => dbQuery.methodicalDelete());
            return new JsonResponse("all deleted");
        }

        //test method -- For Development only
        public ActionResult<JsonResponse> TestMethod(){
            return new BadRequestResult();
            //return reporter.genericReport(dbQuery.getAllSessions());
        }

    }
}