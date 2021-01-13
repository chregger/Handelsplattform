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
        //base adress of easycreditcardservice microservice
        private static readonly string creditcardServiceBaseAddress = "https://handelsplattformiegeasycreditcardservice.azurewebsites.net/";


        public PaymentMethodsController(ILogger<PaymentMethodsController> logger)
        {
            _logger = logger;
        }
        [HttpGet]
        public IEnumerable<string> Get()
        {
            List<string> acceptedPaymentMethods = null;
            _logger.LogError("Accepted Paymentmethods");
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(creditcardServiceBaseAddress);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));



            HttpResponseMessage response = client.GetAsync(creditcardServiceBaseAddress + "/api/AcceptedCreditCards").Result;
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