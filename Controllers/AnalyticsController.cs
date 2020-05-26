using Microsoft.AspNetCore.Mvc;
using analytics.JsonMessage;
using analytics.Models;
using analytics.Controllers.ControllerMethods;
using System.Collections.Generic;
using System.Linq;
using analytics.Queries;
using analytics.dtos;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace analytics.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AnalyticsController : ControllerBase
    {
        private AnalyticsControllerMethods methods;
        private AnalyticsQueries dbQuery;

        public AnalyticsController( AnalyticsQueries _dbQuery ){
            dbQuery = _dbQuery;
            methods = new AnalyticsControllerMethods(dbQuery);
        }

        [HttpPost]
        [Route("create")]
        public async Task<ActionResult<GenericSession>> GenUserSession([FromBody] GenericSession _NewSession){
            return await methods.genUserSessionMethod(_NewSession);
        }

        [HttpPost]
        [Route("update")]
        public async Task<ActionResult<JsonResponse>> UpdateSession([FromBody] GenericSession _CurrentSession){
            return await methods.UpdateSessionMethod(_CurrentSession);
        }

        [Route("read")]
        public async Task<ActionResult<List<GenericSession>>> ReturnSessions(){
            return await methods.ReturnSessionsMethod();
        }

        [HttpDelete]
        [Route("delete")]
        public async Task<ActionResult<JsonResponse>> DeleteAll(){
            return await methods.DeleteAllMethod();
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Route("test")]
        public ActionResult<JsonResponse> test(){
            return new BadRequestResult();//methods.TestMethod();
        }
    }
}