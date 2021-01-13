using LoggingService.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace LoggingService.Controllers
{
    [Route("api/Logging")]
    [ApiController]
    public class LoggingController : Controller
    {
        private readonly string[] _log = { "empty" };

        [HttpGet]
        public IEnumerable<string> Get()
        {
            return _log;
        }

        /// <summary>
        /// Add a message to the logging service
        /// </summary>
        /// <param name="log"></param>
        /// <returns></returns>
        [HttpPost]
        public string Post([FromBody] LogMessage log)
        {
            _log[0] = log.Message + " logged!";
            Console.WriteLine("Log: " + log.Message);

            return _log[0];
        }
    }
}
