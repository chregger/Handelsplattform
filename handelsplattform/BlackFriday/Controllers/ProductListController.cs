﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BlackFriday.Controllers
{
    [Route("api/ProductListController")]
    [Produces("application/json")]

    public class ProductListController : Controller
    {
        private readonly ILogger<ProductListController> _logger;
        //base address of product catalog micro service
        private const string ServiceBaseAddress = "https://handelsplattformproductcatalog.azurewebsites.net";

        public ProductListController(ILogger<ProductListController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<string> Get()
        {
            List<string> productList = null;
            _logger.LogError("Product list in");

            var client = new HttpClient
            {
                BaseAddress = new Uri(ServiceBaseAddress)
            };

            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var response = client.GetAsync(ServiceBaseAddress + "/api/ProductCatalog").Result;
            if (response.IsSuccessStatusCode)
            {
                productList = response.Content.ReadAsAsync<List<string>>().Result;
            }

            if (productList != null)
            {
                foreach (var item in productList)
                {
                    _logger.LogError("Product {0}", item);
                }
            }

            return productList;
        }
    }
}
