using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoggingService.Models
{
    public class LogMessage
    {
        public string Message { get; set; }

        public LogMessage(string message)
        {
            this.Message = message;
        }
    }
}
