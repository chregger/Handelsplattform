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
    [Route("api/ProductListController")]
    [Produces("application/json")]

    public class ProductListController : Controller
    {
        private readonly ILogger<ProductListController> _logger;
        private static readonly string _serviceBaseAddress = "https://productcatalog20201201091428.azurewebsites.net/";

        public ProductListController(ILogger<ProductListController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<string> Get()
        {
            List<string> productList = null;
            _logger.LogError("Product List IN");

            var client = new HttpClient
            {
                BaseAddress = new Uri(_serviceBaseAddress)
            };

            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var response = client.GetAsync(_serviceBaseAddress + "/api/ProductCatalog").Result;
            if (response.IsSuccessStatusCode)
            {
                productList = response.Content.ReadAsAsync<List<string>>().Result;
            }

            foreach (var item in productList)
            {
                _logger.LogError("Product {0}", item);

            }

            //productList.Add("Dummy added: /api/productcatalog call successful");
            return productList;
        }
    }
}
