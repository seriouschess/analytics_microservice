using Microsoft.AspNetCore.Mvc;

using System;
using System.IO;
using System.Collections;

namespace analytics.Controllers.ControllerMethods
{
    [ApiController]
    [Route("[controller]")]
    public class CdnController: ControllerBase
    {
         public CdnController( ){ }

         [HttpGet]
         [Route("logscript")]
         public ActionResult hi(){
             string path = $"{Environment.CurrentDirectory}/ClientScript/static/amfe.js";
          
            if(System.IO.File.Exists(path))
            {
                System.IO.FileStream stream = System.IO.File.OpenRead(path);
                return new FileStreamResult(stream, "application/javascript");
            }
            return StatusCode(500);
         }
    }
}