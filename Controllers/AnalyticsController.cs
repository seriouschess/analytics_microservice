using Microsoft.AspNetCore.Mvc;
using analytics.JsonMessage;
using analytics.Models;
using analytics.Controllers.ControllerMethods;
using System.Collections.Generic;
using System.Linq;
using analytics.Queries;

namespace analytics.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AnalyticsController : ControllerBase
    {
        private AnalyticsControllerMethods methods;
        private AnalyticsQueries dbQuery;

        public AnalyticsController( AnalyticsQueries _dbQuery){
            dbQuery = _dbQuery;
            methods = new AnalyticsControllerMethods(dbQuery);
        }

        [HttpGet]
        [Route("auth")]
        public ActionResult<JsonResponse> GenAuth(){
            return methods.getTokenMethod();
        }

        [HttpPost]
        [Route("create")]
        public ActionResult<GenericSession> GenUserSession([FromBody] GenericSession _NewSession){
            return methods.genUserSessionMethod(_NewSession);
        }

        [HttpPost]
        [Route("update")]
        public ActionResult<JsonResponse> UpdateSession([FromBody] GenericSession _CurrentSession){
            return methods.UpdateSessionMethod(_CurrentSession);
        }

        [Route("read")]
        public ActionResult<List<GenericSession>> ReturnSessions(){
            return methods.ReturnSessionsMethod();
        }

        [HttpDelete]
        [Route("delete")]
        public ActionResult<JsonResponse> DeleteAll(){
            return methods.DeleteAllMethod();
        }
    }
}