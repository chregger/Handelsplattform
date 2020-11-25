using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;


namespace GenericItemCatalog.Controllers
{
    [Route("api/ProductCatalog")]
    public class ProductCatalog : Controller
    {
        string[] productcatalog = new string[] { "Product 1", "Product 2", "Product 3" }; 

        [HttpGet]
        public IEnumerable<string> Get()
        {
            return productcatalog;
        }

    }
}
