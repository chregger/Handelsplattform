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
        private readonly string[] _log = { "empty" };

        [HttpGet]
        public IEnumerable<string> Get()
        {
            return _log;
        }

        [HttpPost]
        public IEnumerable<string> Post([FromBody] Log l)
        {
            _log[0] = l.Value+" logged!";
            //do something meaningful with the received log
            Console.WriteLine("Log: " + l.Value);

            return _log;
        }
    }
}
