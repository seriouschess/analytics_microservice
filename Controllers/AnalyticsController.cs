using Microsoft.AspNetCore.Mvc;
using analytics.JsonMessage;
using analytics.Models;
using analytics.Controllers.ControllerMethods;
using System.Collections.Generic;
using System.Linq;

namespace analytics.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AnalyticsController : ControllerBase
    {
        private AnalyticsControllerMethods methods;
        private AnalyticsContext dbContext;
        public AnalyticsController(AnalyticsContext _dbContext){
            dbContext = _dbContext;
            methods = new AnalyticsControllerMethods();
        }

        [HttpGet]
        [Route("auth")]
        public ActionResult<JsonResponse> GenAuth(){
            return methods.getTokenMethod();
        }

        [HttpPost]
        [Route("create")]
        public ActionResult<GenericSession> GenUserSession([FromBody] GenericSession _NewSession){
            GenericSession NewSession = new GenericSession();
            NewSession.time_on_homepage = _NewSession.time_on_homepage;
            dbContext.Add(NewSession);
            // OR dbContext.Users.Add(newUser);
            dbContext.SaveChanges();
            return NewSession;
        }

        [HttpPost]
        [Route("update")]
        public ActionResult<JsonResponse> UpdateSession([FromBody] GenericSession _CurrentSession){
            System.Console.WriteLine($"Session ID: {_CurrentSession.session_id}");
            GenericSession CurrentSession = dbContext.SessionGs.Where( x => x.session_id == _CurrentSession.session_id).FirstOrDefault();
            CurrentSession.time_on_homepage = _CurrentSession.time_on_homepage;
            dbContext.SaveChanges();
            return new JsonResponse("Session Updated");
        }

        [Route("read")]
        public ActionResult<List<GenericSession>> ReturnSessions(){
            List<GenericSession> output = dbContext.SessionGs.ToList();
            return output;
        }

        [HttpDelete]
        [Route("delete")]
        public ActionResult<JsonResponse> DeleteAll(){
            List<GenericSession> output = dbContext.SessionGs.ToList();
            foreach (GenericSession item in output){
                dbContext.SessionGs.Remove(item);
                dbContext.SaveChanges();
            }

            return new JsonResponse("all deleted");
        }
    }
}