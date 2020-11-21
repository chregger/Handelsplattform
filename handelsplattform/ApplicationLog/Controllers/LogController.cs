using System;
using System.Collections.Generic;
using ApplicationLog.Models;
using Microsoft.AspNetCore.Mvc;

namespace ApplicationLog.Controllers
{
    [Route("api/LogController")]
    [ApiController]
    public class LogController : ControllerBase
    {
        //not a really usefull controller, local variable should be something different in productive

        string[] log = new string[] { "empty" };

        [HttpGet]
        public IEnumerable<string> Get()
        {
            return log;
            
        }

        [HttpPost]
        public IEnumerable<string> Post([FromBody] Log l)
        {

            log[0] = l.Value+" logged!";
            //do something meaningfull with the received log
            Console.WriteLine("Log: " + l.Value);

            return log;
        }
    }
}
