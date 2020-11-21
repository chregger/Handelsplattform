using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;

namespace BlackFriday.Controllers
{
    [Produces("application/json")]
    [Route("api/PaymentMethods")]
    public class PaymentMethodsController : Controller
    {
        //https://docs.microsoft.com/en-us/aspnet/web-api/overview/advanced/calling-a-web-api-from-a-net-client
        private readonly ILogger<PaymentMethodsController> _logger;
        private static readonly string _creditcardServiceBaseAddress = "http://iegeasycreditcardservice.azurewebsites.net/";
        
        public PaymentMethodsController(ILogger<PaymentMethodsController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<string> Get()
        {
            List<string> acceptedPaymentMethods = null;
            _logger.LogError("Accepted Paymentmethods");

            var client = new HttpClient
            {
                BaseAddress = new Uri(_creditcardServiceBaseAddress)
            };

            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var response = client.GetAsync(_creditcardServiceBaseAddress + "/api/AcceptedCreditCards").Result;
            if (response.IsSuccessStatusCode)
            {
                acceptedPaymentMethods = response.Content.ReadAsAsync<List<string>>().Result;
            }

            foreach (var item in acceptedPaymentMethods)
            {
                _logger.LogError("Paymentmethod {0}", new object[] { item });

            }

            return acceptedPaymentMethods;
        }
    }
}