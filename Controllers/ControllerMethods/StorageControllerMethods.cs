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
    public class StorageControllerMethods
    {
        private AnalyticsQueries dbQuery;
        private Authenticator auth;
        private DataFormatter formatter;
        private Reporter reporter;

        public StorageControllerMethods( AnalyticsQueries _dbQuery ){
            dbQuery = _dbQuery;
            auth = new Authenticator(dbQuery);
            formatter = new DataFormatter();
            reporter = new Reporter();
        }

        //CRUD actions
        public async Task<ActionResult<GenericSession>> genUserSessionMethod(GenericSession _NewSession){
            bool verdict = false;
            GenericSession NewSession = new GenericSession();

            try{ //load object
                verdict = auth.authenticateGeneralUse(_NewSession.token);
                NewSession.time_on_homepage = _NewSession.time_on_homepage;
                NewSession.url = _NewSession.url;
                NewSession.token = auth.GenerateToken();
            }catch{ //invalid object
                return new BadRequestResult();
            }
            
            if(verdict == true){                
                await Task.Run(() => dbQuery.addSession(NewSession));
                return NewSession;
            }else{                //authentication fail
                return new BadRequestResult();
            } 
        }

        public async Task<ActionResult<JsonResponse>> UpdateSessionMethod( GenericSession CurrentSession ){
            if(auth.validateToken(CurrentSession.session_id, CurrentSession.token)){
                await Task.Run(() => dbQuery.updateSession( CurrentSession ));
                return new JsonResponse("Session Updated");
            }else{  //auth fail
                return new BadRequestResult();
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