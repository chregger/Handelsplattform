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
        private readonly ILogger<PaymentMethodsController> _logger;
        //base address of credit card service micro-service
        private const string CreditcardServiceBaseAddress = "https://handelsplattformiegeasycreditcardservice.azurewebsites.net/";

        public PaymentMethodsController(ILogger<PaymentMethodsController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<string> Get()
        {
            List<string> acceptedPaymentMethods = null;
            _logger.LogError("Accepted payment methods");

            var client = new HttpClient
            {
                BaseAddress = new Uri(CreditcardServiceBaseAddress)
            };

            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var response = client.GetAsync(CreditcardServiceBaseAddress + "/api/AcceptedCreditCards").Result;
            if (response.IsSuccessStatusCode)
            {
                acceptedPaymentMethods = response.Content.ReadAsAsync<List<string>>().Result;
            }

            if (acceptedPaymentMethods != null)
            {
                foreach (var item in acceptedPaymentMethods)
                {
                    _logger.LogError("Payment method {0}", item);
                }
            }

            return acceptedPaymentMethods;
        }
    }
}