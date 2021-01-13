using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace ProductCatalog.Controllers
{
    [Route("api/ProductCatalog")]
    public class ProductCatalog : Controller
    {
        private readonly string[] _productCatalog = { "Product 1", "Product 2", "Product 3" };

        [HttpGet]
        public IEnumerable<string> Get()
        {
            return _productCatalog;
        }
    }
}
