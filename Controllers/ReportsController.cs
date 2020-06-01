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

//fluentvalidation
using FluentValidation.Results;
using analytics.Validators;

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
        [Route("for_domain/on_date/summary")]
        public async Task<ActionResult<DomainReportSummary>> DomainSummaryByDate([FromBody] DateReportRequest request){
            DateReportValidator validator = new DateReportValidator();
            ValidationResult verdict = validator.Validate(request);
            if(verdict.IsValid){
                string domain = request.domain;
                DateTime date = DateTime.Parse($"{request.year}-{request.month}-{request.day}");
                return await methods.ReportByDateMethod(domain, date);
            }else{
                DateReportRequestError response = new DateReportRequestError(verdict);
                return StatusCode(400, response);
            }
        }

        [HttpGet]
        [Route("for_domain/on_month/summary")]
        public async Task<ActionResult<DomainReportSummary>> DomainReportByMonth([FromBody] MonthReportRequest request){
            MonthReportValidator validator = new MonthReportValidator();
            ValidationResult verdict = validator.Validate(request);
            if(verdict.IsValid){
                string domain = request.domain;
                DateTime month = DateTime.Parse($"{request.year}-{request.month}");
                return await methods.ReportByMonthMethod(domain, month);
            }else{
                MonthReportRequestError response = new MonthReportRequestError(verdict);
                return StatusCode(400, response);
            }
        }

        [HttpGet]
        [Route("for_domain/between_dates/summary")]
        public async Task<ActionResult<DomainReportSummary>> DomainReportBetweenDates([FromBody] DateRangeReportRequest request){
            DateRangeReportValidator validator = new DateRangeReportValidator();
            ValidationResult verdict = validator.Validate(request);
            if(verdict.IsValid){
                string domain = request.domain;
                DateTime date_one = DateTime.Parse($"{request.date_one.year}-{request.date_one.month}-{request.date_one.day}");
                DateTime date_two = DateTime.Parse($"{request.date_two.year}-{request.date_two.month}-{request.date_two.day}");
                return await methods.ReportBetweenDatesMethod(domain, date_one, date_two);
            }else{
                DateRangeReportRequestError response = new DateRangeReportRequestError(verdict);
                return StatusCode(400, response);
            }
        }

        //raw data requests
        [HttpGet]
        [Route("by_domain/raw/{domain}")]
        public async Task<ActionResult<List<GenericSession>>> RawDomainReport(string domain){
            System.Console.WriteLine(domain);
            return await methods.getAllByDomain(domain);
        }

        [HttpGet]
        [Route("for_domain/on_date/raw")]
        public async Task<ActionResult<List<GenericSession>>> RawDomainReportByDate([FromBody] DateReportRequest request){
            DateReportValidator validator = new DateReportValidator();
            ValidationResult verdict = validator.Validate(request);
            if(verdict.IsValid){
                string domain = request.domain;
                DateTime date = DateTime.Parse($"{request.year}-{request.month}-{request.day}");
                return await methods.RawByDateMethod(domain, date);
            }else{
                DateReportRequestError response = new DateReportRequestError(verdict);
                return StatusCode(400, response);
            }
        }

        [HttpGet]
        [Route("for_domain/on_month/raw")]
        public async Task<ActionResult<List<GenericSession>>> RawDomainReportByMonth([FromBody] MonthReportRequest request){
            MonthReportValidator validator = new MonthReportValidator();
            ValidationResult verdict = validator.Validate(request);
            if(verdict.IsValid){
                string domain = request.domain;
                DateTime month = DateTime.Parse($"{request.year}-{request.month}");
                return await methods.RawByMonthMethod(domain, month);
            }else{
                MonthReportRequestError response = new MonthReportRequestError(verdict);
                return StatusCode(400, response);
            }
        }

        [HttpGet]
        [Route("for_domain/between_dates/raw")]
        public async Task<ActionResult<List<GenericSession>>> RawDomainReportBetweenDates([FromBody] DateRangeReportRequest request){
            DateRangeReportValidator validator = new DateRangeReportValidator();
            ValidationResult verdict = validator.Validate(request);
            if(verdict.IsValid){
                string domain = request.domain;
                DateTime date_one = DateTime.Parse($"{request.date_one.year}-{request.date_one.month}-{request.date_one.day}");
                DateTime date_two = DateTime.Parse($"{request.date_two.year}-{request.date_two.month}-{request.date_two.day}");
                return await methods.RawBetweenDatesMethod(domain, date_one, date_two);
            }else{
                DateRangeReportRequestError response = new DateRangeReportRequestError(verdict);
                return StatusCode(400, response);
            }
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
