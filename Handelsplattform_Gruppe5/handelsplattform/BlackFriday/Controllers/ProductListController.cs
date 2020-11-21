using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

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
        private static readonly string serviceBaseAddress = "http://ppgenericitemcatalog.azurewebsites.net/";

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
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(serviceBaseAddress);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

     // --- Step 6:  Call API and save into our list      
            HttpResponseMessage response = client.GetAsync(serviceBaseAddress + "/api/GenericItemCatalog").Result;
            if (response.IsSuccessStatusCode)
            {
                productList = response.Content.ReadAsAsync<List<string>>().Result;
            }

            foreach (var item in productList)
            {
                _logger.LogError("Product {0}", new object[] { item });

            }
     // --- Step 7: return list, Added a dummy item to see if call was successfull
            productList.Add("Dummy added: /api/GenericItemCatalog call successfull");
            return productList;
        }
    }

}

