using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Transactions;
using Microsoft.Extensions.Logging;
using IEGEasyCreditCardService.Models;

namespace IEGEasyCreditCardService.Controllers
{
    [Route("api/[controller]")]
    public class CreditcardTransactionsController : Controller
    {
        private readonly ILogger<CreditcardTransactionsController> _logger;

        public CreditcardTransactionsController(ILogger<CreditcardTransactionsController> logger)
        {
            _logger = logger;
        }
        [HttpGet]
        public string Get(int id)
        {

            return "value" + id;
        }
        /// <summary>
        /// Add a creditcard transaction 
        /// </summary>
        /// <param name="creditcardTransaction"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Post([FromBody]CreditcardTransaction creditcardTransaction)
        {
            _logger.LogError("TransactionInfo Number: {0} Amount:{1} Receiver: {2}", new object[] { creditcardTransaction.CreditcardNumber, creditcardTransaction.Amount,creditcardTransaction.ReceiverName });
            return CreatedAtAction("Get", new { id = System.Guid.NewGuid() });
        }
        
    }
}
