using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Paymentservice.Models;

namespace Paymentservice.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PaymentServiceController : ControllerBase
    {
        // later on this should be build together at get, not beforehand, but for now lets say this are the objekt that an ID in Get would deliver
        CreditcardTransaction c_csv = new CreditcardTransaction("csv", "123", "Amex", 50.0, "Receiver");
        CreditcardTransaction c_json = new CreditcardTransaction("json", "123", "Amex", 50.0, "Receiver");
        CreditcardTransaction c_xml = new CreditcardTransaction("xml", "123", "Amex", 50.0, "Receiver");

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
        public IActionResult Post([FromBody]CreditcardTransaction creditcardTransaction)
        {
            //xml formatter funktioniert leider nicht so....conversion error

            if (creditcardTransaction == null)
                return NoContent();

            return Ok(creditcardTransaction);
        }
    }
}
