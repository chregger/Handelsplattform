using Microsoft.AspNetCore.Mvc;
using PaymentService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentService.Controllers
{
    [ApiController]
    [Route("api/Payment")]
    public class PaymentController : Controller
    {
        Transaction c_csv = new Transaction("csv", "123", "Amex", 50.0, "Receiver");
        Transaction c_json = new Transaction("json", "123", "Amex", 50.0, "Receiver");
        Transaction c_xml = new Transaction("xml", "123", "Amex", 50.0, "Receiver");

        [HttpGet]
        public IActionResult Get()
        {
            string ContentType = Request.ContentType;

            switch (ContentType)
            {
                case string x when x.Contains("json"):
                    return Ok(c_json);
                case string x when x.Contains("xml"):
                    {
                        Response.ContentType = "application/xml";
                        return new OkObjectResult(c_xml);
                    }
                case string x when x.Contains("csv"):
                    return new OkObjectResult(c_xml.CreditcardNumber + ";" + c_xml.CreditcardType + ";" + c_xml.Amount + ";" + c_xml.ReceiverName);
                default:
                    return null;
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody] Transaction creditcardTransaction)
        {
            if (creditcardTransaction == null)
                return NoContent();

            return Ok(creditcardTransaction);
        }
    }
}
