using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace analytics.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AdminController: ControllerBase
    {
        private readonly IConfiguration configuration;
        private readonly JwtGenerator jwtGenerator;

       public AdminController(IConfiguration config){
           jwtGenerator = new JwtGenerator();
       }

       [HttpPost]
       [Route("token/get")]
       public IActionResult GetToken( [FromBody] LoginModel loginModel ){

           JwtGenerator token = jwtGenerator
            .AddClaim(new Claim("Email", loginModel.email))
            .AddClaim(new Claim("Password", loginModel.password));

            return Ok(new {
                Token = token.GetToken(),
                ExpirationInUnixTime = token.GetTokenExpirationInUnixTime
            });

       }

       [HttpGet]
       [Authorize]
       [Route("secure")]
       public ActionResult<string> GetSecureEndpoint(){
            return "success";
       }
    }

    public class JwtGenerator{
        private readonly JwtHeader jwtHeader;
        private readonly List<Claim> jwtClaims;
        private readonly DateTime jwtDate;
        private readonly int tokenLifetimeInSeconds;

        public JwtGenerator(){
            var credentials = new SigningCredentials(
                key: new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes("1234567890")
                ),
                algorithm: SecurityAlgorithms.HmacSha256
            );

            jwtHeader = new JwtHeader(credentials);
            jwtClaims = new List<Claim>();
            jwtDate = DateTime.UtcNow;
            tokenLifetimeInSeconds = int.Parse("3600");
        }

        public JwtGenerator AddClaim(Claim claim){
            jwtClaims.Add(claim);
            return this;
        }

        public long GetTokenExpirationInUnixTime => new DateTimeOffset(
            jwtDate.AddSeconds(tokenLifetimeInSeconds)
        ).ToUnixTimeMilliseconds();

        public string GetToken() {
            var jwt = new JwtSecurityToken(
                jwtHeader,
                new JwtPayload(
                    audience: "identityapp",
                    issuer:"identityapp",
                    notBefore: jwtDate,
                    expires: jwtDate.AddSeconds(tokenLifetimeInSeconds),
                    claims: jwtClaims
                )
            );

            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }
    }

    public class LoginModel
    {
        public string email {get;set;}
        public string password {get;set;}
    }
}