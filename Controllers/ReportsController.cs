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
using System;
using analytics.dtos.RequestDtos;


namespace analytics.Controllers{
        [ApiController]
        [Route("[controller]")]
        public class ReportsController : ControllerBase
        {
            private AnalyticsControllerMethods methods;

            private AnalyticsQueries dbQuery;

            public ReportsController( AnalyticsQueries _dbQuery ){
                dbQuery = _dbQuery;
                methods = new AnalyticsControllerMethods( dbQuery );
            }

            [Route("read")]
            public async Task<ActionResult<List<GenericSession>>> ReturnSessions(){
                return await methods.ReturnSessionsMethod();
            }

            [Route("by_domain/{domain}")]
            public async Task<ActionResult<List<GenericSession>>> ReportByDomain(string domain){
                System.Console.WriteLine(domain);
                return await methods.getAllByDomain(domain);
            }

            [Route("on_domain_by_date")]
            public async Task<ActionResult<List<GenericSession>>> ReportByDate([FromBody] ReportRequest request){
                DateTime date;
                string domain;
                 try{
                     domain = request.domain;
                     date = DateTime.Parse($"{request.year}-{request.month}-{request.day}");
                 }catch{
                     JsonReportRequestError response = new JsonReportRequestError();
                     System.Console.WriteLine($"response: {response.message}");
                    return StatusCode(400, response);
                 }
                System.Console.WriteLine($"Domain: {domain} Date: {date}");

                List<GenericSession> reportSessions = await Task.Run(() => dbQuery.getSessionsForDomainByDate( domain, date));
                
                return reportSessions;
            }

            [HttpPost]
            [Route("on_domain_by_month")]
            public async Task<ActionResult<List<GenericSession>>> ReportByMonth([FromBody] ReportRequest request){
                DateTime date;
                string domain;
                // try{
                     domain = request.domain;
                     date = DateTime.Parse($"{request.year}-{request.month}");
                // }catch{
                //     return new BadRequest("Incomplete information payload. Body must include: domain,year,month,day");
                // }
                System.Console.WriteLine($"Domain: {domain} Date: {date}");

                List<GenericSession> reportSessions = await Task.Run(() => dbQuery.getSessionsForDomainByMonth( domain, date));
                
                return reportSessions;
            }

            [HttpGet]
            [ProducesResponseType(StatusCodes.Status200OK)]
            [ProducesResponseType(StatusCodes.Status400BadRequest)]
            [Route("test")]
            public ActionResult<JsonResponse> test(){
                return new BadRequestResult(); //methods.TestMethod();
            }
        }
}
