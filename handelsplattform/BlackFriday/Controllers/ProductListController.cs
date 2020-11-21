using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BlackFriday.Controllers
{
    // --- Step 1: Define Route and Result
    [Route("api/ProductListController")]
    [Produces("application/json")]

    public class ProductListController : Controller
    {
        // --- Step 2: Prepare URL and Logger
        private readonly ILogger<ProductListController> _logger;
        private static readonly string _serviceBaseAddress = "http://ppgenericitemcatalog.azurewebsites.net/";

        public ProductListController(ILogger<ProductListController> logger)
        {
            _logger = logger;
        }

        // *---=== Erweiterung 1 Punkt 2. ===---*
        // --- Step 3: The Get
        [HttpGet]
        public IEnumerable<string> Get()
        {
            // --- Step 4: Use a List to save Results
            List<string> productList = null;//= new string[] { "Diners", "Master" };
            _logger.LogError("Product List IN");

            // --- Step 5: Prepare for response
            var client = new HttpClient
            {
                BaseAddress = new Uri(_serviceBaseAddress)
            };

            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            // --- Step 6:  Call API and save into our list      
            var response = client.GetAsync(_serviceBaseAddress + "/api/GenericItemCatalog").Result;
            if (response.IsSuccessStatusCode)
            {
                productList = response.Content.ReadAsAsync<List<string>>().Result;
            }

            foreach (var item in productList)
            {
                _logger.LogError("Product {0}", item);

            }

            // --- Step 7: return list, Added a dummy item to see if call was successful
            productList.Add("Dummy added: /api/GenericItemCatalog call successful");
            return productList;
        }
    }

}

