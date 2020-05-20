using System.Collections.Generic;
using analytics.JsonMessage;
using analytics.Models;
using analytics.Queries;

namespace analytics.Controllers.ControllerMethods
{
    public class AnalyticsControllerMethods
    {
        private AnalyticsQueries dbQuery;
        public AnalyticsControllerMethods( AnalyticsQueries _dbQuery ){
            dbQuery = _dbQuery;
         }

        public JsonResponse getTokenMethod(){
            return new JsonResponse("aflsidjbf;lasdjbflkasdjbf");
        }

        public GenericSession genUserSessionMethod(GenericSession _NewSession){
            GenericSession NewSession = new GenericSession();
            NewSession.time_on_homepage = _NewSession.time_on_homepage;
            NewSession.url = _NewSession.url;
            dbQuery.addSession(NewSession);
            return NewSession;
        }

        public JsonResponse UpdateSessionMethod( GenericSession CurrentSession ){
            System.Console.WriteLine($"Session ID: { CurrentSession.session_id }");
            dbQuery.updateSession( CurrentSession );
            return new JsonResponse("Session Updated");
        }


        public List<GenericSession> ReturnSessionsMethod(){
            return dbQuery.getAllSessions();
        }

        public JsonResponse DeleteAllMethod(){
            dbQuery.methodicalDelete();
            return new JsonResponse("all deleted");
        }
    }
}