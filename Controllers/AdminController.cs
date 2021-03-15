using Microsoft.AspNetCore.Mvc;

namespace analytics.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AdminController: ControllerBase
    {
       public AdminController(){}

       [HttpGet]
       [Route("secure")]
       public ActionResult<string> GetSecureEndpoint(){
           return "success";
       }
    }
}