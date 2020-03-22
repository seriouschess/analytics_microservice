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

        [Route("create")]
        public ActionResult<JsonResponse> GenUserSession(){
            GenericSession NewSession = new GenericSession();
            NewSession.time_on_homepage = 11;
            dbContext.Add(NewSession);
            // OR dbContext.Users.Add(newUser);
            dbContext.SaveChanges();
            return new JsonResponse("session uploaded");
        }

        [Route("read")]
        public ActionResult<List<GenericSession>> ReturnSessions(){
            List<GenericSession> output = dbContext.SessionGs.ToList();
            return output;
        }
    }
}