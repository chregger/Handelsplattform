using LoggingService.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoggingService.Controllers
{
    [Route("api/Logging")]
    [ApiController]
    public class LoggingController : Controller
    {
        private string[] _log = { "empty" };

        [HttpGet]
        public IEnumerable<string> Get()
        {
            return _log;
        }

        [HttpPost]
        public string Post([FromBody] LogMessage log)
        {
            _log[0] = log.Message + " logged!";
            Console.WriteLine("Log: " + log.Message);

            return _log[0];
        }
    }
}
