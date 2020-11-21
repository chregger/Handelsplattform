using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;


namespace GenericItemCatalog.Controllers
{
    [Route("api/GenericItemCatalog")]
    public class GenericItemCatalog : Controller
    {
        string[] genericcatalog = new string[] { "Generic Item 1", "Generic Item 2", "Generic Item 3" }; // oder Objekt/Liste aus einer anderen Klasse verwenden

        // *---=== Arbeitsblatt Erweiterung 1 Punkt 2. ===---*
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return genericcatalog;
        }

    }
}
