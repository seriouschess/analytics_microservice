using System;
using System.Collections.Generic;
using analytics.Classes;
using analytics.dtos;
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
        public List<GenericSession> ReturnSessionsMethod(){
            return dbQuery.getAllSessions();
        }

        //Url related Queries

        public List<GenericSession> getAllByDomain(string domain){
            domain = formatter.stripDomain(domain);
            return dbQuery.getSessionsByDomain(domain);
        }

        public List<GenericSession> getAllByUrl(string url){ //maybe validate url regex to ensure . and / exist?
            return dbQuery.getSessionsByUrl(url);
        }

        //DateTime Queries

        public List<GenericSession> getAllBeforeDateTime(DateTime date){
            return dbQuery.getSessionsBeforeDateTime(date);
        }

        public List<GenericSession> getAllAfterDateTime(DateTime date){
            return dbQuery.getSessionsAfterDateTime(date);
        }

        public List<GenericSession> getAllInDateTimeRange(DateTime min_date, DateTime max_date){
            return dbQuery.getSessionsInDateTimeRange(min_date, max_date);
        }

        public List<GenericSession> getAllByDate(int year, int month, int day){
            DateTime minDate = new DateTime(year, month, day);
            return dbQuery.getSessionsByDate(minDate);
        }

        //CRUD actions
        public GenericSession genUserSessionMethod(GenericSession _NewSession){
            if(auth.authenticateGeneralUse(_NewSession.token)){
                GenericSession NewSession = new GenericSession();
                NewSession.time_on_homepage = _NewSession.time_on_homepage;
                NewSession.url = _NewSession.url;
                NewSession.token = auth.GenerateToken();
                dbQuery.addSession(NewSession);
                return NewSession;
            }else{                //authentication fail
                return new GenericSession(); 
            } 
        }

        public JsonResponse UpdateSessionMethod( GenericSession CurrentSession ){
            if(auth.validateToken(CurrentSession.session_id, CurrentSession.token)){
                dbQuery.updateSession( CurrentSession );
                return new JsonResponse("Session Updated");
            }else{  //auth fail
                return new JsonResponse("There's nothing to fear but fear itself.");
            }
            
        }

        public JsonResponse DeleteAllMethod(){
            dbQuery.methodicalDelete();
            return new JsonResponse("all deleted");
        }

        //test method -- For Development only
        public ReportDto TestMethod(){
            return reporter.genericReport(dbQuery.getAllSessions());
        }

    }
}