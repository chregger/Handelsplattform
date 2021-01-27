using Microsoft.AspNetCore.Mvc;
using PaymentService.Models;

namespace PaymentService.Controllers
{
    [ApiController]
    [Route("api/Payment")]
    public class PaymentController : Controller
    {
        /// <summary>
        /// Read content type from header and create response based on the type
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Get()
        {
            var transaction = new Transaction("12345", "Master", 50.0, "Receiver");
            return Ok(transaction);
            //var contentType = Request.ContentType;

            //switch (contentType)
            //{
            //    case string x when x.Contains("json"):
            //        return Ok(_json);
            //    case string x when x.Contains("xml"):
            //        {
            //            Response.ContentType = "application/xml";
            //            return new OkObjectResult(_xml);
            //        }
            //    case string x when x.Contains("csv"):
            //        return new OkObjectResult(_xml.CreditCardNumber + ";" + _xml.CreditCardType + ";" + _xml.Amount + ";" + _xml.ReceiverName);
            //    default:
            //        return NoContent();
            //}
        }

        [HttpPost]
        public IActionResult Post([FromBody] Transaction creditcardTransaction)
        {
            if (creditcardTransaction == null)
            {
                return NoContent();
            }

            return Ok(creditcardTransaction);
        }
    }
}
