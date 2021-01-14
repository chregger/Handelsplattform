using BlackFriday.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Net.Http;
using System.Net.Http.Headers;

namespace BlackFriday.Controllers
{
    [Produces("application/json")]
    [Route("api/CashDesk")]
    public class CashDeskController : Controller
    {
        private readonly ILogger<CashDeskController> _logger;
        //base address of credit card service micro-service
        private const string CreditcardServiceBaseAddress = "https://handelsplattformiegeasycreditcardservice.azurewebsites.net";

        public CashDeskController(ILogger<CashDeskController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Get(string id)
        {
            return Content("OK");
        }

        /// <summary>
        /// Add a credit card transaction to the black friday service
        /// </summary>
        /// <param name="basket"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Post([FromBody] Basket basket)
        {
            _logger.LogError("TransactionInfo Creditcard: {0} Product:{1} Amount: {2}", basket.CustomerCreditCardNumber, basket.Product, basket.AmountInEuro);

            //Create new credit card transaction from body
            var creditCardTransaction = new CreditcardTransaction
            {
                Amount = basket.AmountInEuro,
                CreditcardNumber = basket.CustomerCreditCardNumber,
                ReceiverName = basket.Vendor
            };

            var client = new HttpClient
            {
                BaseAddress = new Uri(CreditcardServiceBaseAddress)
            };

            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            
            var response = client.PostAsJsonAsync(CreditcardServiceBaseAddress + "/api/CreditcardTransactions", creditCardTransaction).Result;
            response.EnsureSuccessStatusCode();


            return CreatedAtAction("Get", new { id = System.Guid.NewGuid() }, creditCardTransaction);
        }
    }
}