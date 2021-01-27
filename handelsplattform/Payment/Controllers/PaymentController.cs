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
