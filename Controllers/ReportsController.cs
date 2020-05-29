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
using analytics.dtos.ErrorDtos;
using analytics.dtos.ReportDtos;

namespace analytics.Controllers{
    [ApiController]
    [Route("[controller]")]
    public class ReportsController : ControllerBase
    {
        private ReportsControllerMethods methods;

        private AnalyticsQueries dbQuery;

        public ReportsController( AnalyticsQueries _dbQuery ){
            dbQuery = _dbQuery;
            methods = new ReportsControllerMethods( dbQuery );
        }

        [Route("read")] //really only for testing - should not be in production
        public async Task<ActionResult<List<GenericSession>>> ReturnSessions(){
            return await methods.ReturnSessionsMethod();
        }

        //summary reports 
        [HttpGet]
        [Route("by_domain/summary/{domain}")]
        public async Task<ActionResult<DomainReportSummary>> DomainSummary(string domain){
            return await methods.getSummaryByDomain(domain);
        }

        [HttpGet]
        [Route("for_domain_on_date/summary")]
        public async Task<ActionResult<DomainReportSummary>> DomainSummaryByDate([FromBody] DateReportRequest request){
            DateTime date;
            string domain;
                try{
                    domain = request.domain;
                    date = DateTime.Parse($"{request.year}-{request.month}-{request.day}");
                }catch{
                    DateReportRequestError response = new DateReportRequestError();
                return StatusCode(400, response);
                }

                return await methods.ReportByDateMethod(domain, date);
        }

        [HttpGet]
        [Route("for_domain_on_month/summary")]
        public async Task<ActionResult<List<GenericSession>>> DomainReportByMonth([FromBody] MonthReportRequest request){
            DateTime month;
            string domain;

            try{
                domain = request.domain;
                month = DateTime.Parse($"{request.year}-{request.month}");
            }catch{
                MonthReportRequestError response = new MonthReportRequestError();
                return StatusCode(400, response);
            }
            
            return await methods.RawByMonthMethod(domain, month);
        }

        [HttpGet]
        [Route("for_domain_between_dates/summary")]
        public async Task<ActionResult<DomainReportSummary>> DomainReportBetweenDates([FromBody] DateRangeReportRequest request){
            DateTime date_one;
            DateTime date_two;
            string domain;

            try{
                domain = request.domain;
                date_one = DateTime.Parse($"{request.date_one.year}-{request.date_one.month}-{request.date_one.day}");
                date_two = DateTime.Parse($"{request.date_two.year}-{request.date_two.month}-{request.date_two.day}");

            }catch{
                DateRangeReportRequestError response = new DateRangeReportRequestError();
                return StatusCode(400, response);
            }

            return await methods.ReportBetweenDatesMethod(domain, date_one, date_two);
        }

        //raw data requests
        [HttpGet]
        [Route("raw/by_domain/{domain}")]
        public async Task<ActionResult<List<GenericSession>>> RawDomainReport(string domain){
            System.Console.WriteLine(domain);
            return await methods.getAllByDomain(domain);
        }

        [HttpGet]
        [Route("for_domain_on_date/raw")]
        public async Task<ActionResult<List<GenericSession>>> RawDomainReportByDate([FromBody] DateReportRequest request){
            DateTime date;
            string domain;
                try{
                    domain = request.domain;
                    date = DateTime.Parse($"{request.year}-{request.month}-{request.day}");
                }catch{
                    DateReportRequestError response = new DateReportRequestError();
                return StatusCode(400, response);
                }

                return await methods.RawByDateMethod(domain, date);
        }

        [HttpGet]
        [Route("for_domain_on_month/raw")]
        public async Task<ActionResult<List<GenericSession>>> RawDomainReportByMonth([FromBody] MonthReportRequest request){
            DateTime month;
            string domain;

            try{
                domain = request.domain;
                month = DateTime.Parse($"{request.year}-{request.month}");
            }catch{
                MonthReportRequestError response = new MonthReportRequestError();
                return StatusCode(400, response);
            }
            
            return await methods.RawByMonthMethod(domain, month);
        }

        [HttpGet]
        [Route("raw/for_domain_between_dates")]
        public async Task<ActionResult<List<GenericSession>>> RawDomainReportBetweenDates([FromBody] DateRangeReportRequest request){
            DateTime date_one;
            DateTime date_two;
            string domain;
            System.Console.WriteLine("doe");

            try{
                System.Console.WriteLine("ray");
                domain = request.domain;
                date_one = DateTime.Parse($"{request.date_one.year}-{request.date_one.month}-{request.date_one.day}");
                date_two = DateTime.Parse($"{request.date_two.year}-{request.date_two.month}-{request.date_two.day}");
            }catch{
                System.Console.WriteLine("me");
                DateRangeReportRequestError response = new DateRangeReportRequestError();
                return StatusCode(400, response);
            }
            System.Console.WriteLine("fa");

            return await methods.RawBetweenDatesMethod(domain, date_one, date_two);
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
