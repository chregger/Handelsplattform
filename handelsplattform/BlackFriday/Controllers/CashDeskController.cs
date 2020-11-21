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
        private static readonly string _creditcardServiceBaseAddress = "http://iegeasycreditcardservice.azurewebsites.net/";

        public CashDeskController(ILogger<CashDeskController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Get(string id)
        {
            return Content("OK");
        }

        [HttpPost]
        public IActionResult Post([FromBody] Basket basket)
        {
            _logger.LogError("TransactionInfo Creditcard: {0} Product:{1} Amount: {2}", basket.CustomerCreditCardnumber, basket.Product, basket.AmountInEuro);

            //Mapping
            var creditCardTransaction = new CreditcardTransaction()
            {
                Amount = basket.AmountInEuro,
                CreditcardNumber = basket.CustomerCreditCardnumber,
                ReceiverName = basket.Vendor
            };

            var client = new HttpClient
            {
                BaseAddress = new Uri(_creditcardServiceBaseAddress)
            };

            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            
            var response = client.PostAsJsonAsync(_creditcardServiceBaseAddress + "/api/CreditcardTransactions", creditCardTransaction).Result;
            response.EnsureSuccessStatusCode();
            
            return CreatedAtAction("Get", new { id = System.Guid.NewGuid() }, creditCardTransaction);
        }
    }
}