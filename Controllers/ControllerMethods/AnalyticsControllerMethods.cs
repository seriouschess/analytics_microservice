using System.Collections.Generic;
using analytics.Classes;
using analytics.JsonMessage;
using analytics.Models;
using analytics.Queries;

namespace analytics.Controllers.ControllerMethods
{
    public class AnalyticsControllerMethods
    {
        private AnalyticsQueries dbQuery;
        private Authenticator auth;
        public AnalyticsControllerMethods( AnalyticsQueries _dbQuery ){
            dbQuery = _dbQuery;
            auth = new Authenticator(dbQuery);
         }

        public GenericSession genUserSessionMethod(GenericSession _NewSession){
            System.Console.WriteLine($"Incoming Token: {_NewSession.token}");
            if(auth.authenticateGeneralUse(_NewSession.token)){
                System.Console.WriteLine("doe");
                GenericSession NewSession = new GenericSession();
                NewSession.time_on_homepage = _NewSession.time_on_homepage;
                NewSession.url = _NewSession.url;
                NewSession.token = auth.GenerateToken();
                dbQuery.addSession(NewSession);
                return NewSession;
            }else{                //authentication fail
                System.Console.WriteLine("ray");
                return new GenericSession(); 
            }
           
        }

        public JsonResponse UpdateSessionMethod( GenericSession CurrentSession ){
            if(auth.validateToken(CurrentSession.session_id, CurrentSession.token)){
                System.Console.WriteLine($"Session ID: { CurrentSession.session_id }");
                dbQuery.updateSession( CurrentSession );
                return new JsonResponse("Session Updated");
            }else{  //auth fail
                return new JsonResponse("There's nothing to fear but fear itself.");
            }
            
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