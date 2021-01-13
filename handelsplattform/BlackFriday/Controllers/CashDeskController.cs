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
        //baseadress of creditcardservice microservice
        private static readonly string creditcardServiceBaseAddress = "https://handelsplattformiegeasycreditcardservice.azurewebsites.net";

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
        /// Add a Creditcardtransaction to the blackfriday service
        /// </summary>
        /// <param name="basket"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Post([FromBody] Basket basket)
        {
            _logger.LogError("TransactionInfo Creditcard: {0} Product:{1} Amount: {2}", new object[] { basket.CustomerCreditCardnumber, basket.Product, basket.AmountInEuro });

            //Create new CreditcardTransaction from body
            CreditcardTransaction creditCardTransaction = new CreditcardTransaction()
            {
                Amount = basket.AmountInEuro,
                CreditcardNumber = basket.CustomerCreditCardnumber,
                ReceiverName = basket.Vendor
            };

            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(creditcardServiceBaseAddress);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = client.PostAsJsonAsync(creditcardServiceBaseAddress + "/api/CreditcardTransactions", creditCardTransaction).Result;
            response.EnsureSuccessStatusCode();


            return CreatedAtAction("Get", new { id = System.Guid.NewGuid() }, creditCardTransaction);
        }
    }
}