using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace IEGEasyCreditCardService.Controllers
{
    [Route("api/[controller]")]
    public class AcceptedCreditCardsController : Controller
    {
        /// <summary>
        /// Get all available ´credit cards
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new[] { "American", "Diners", "Master", "Visa", "Blue Monday" };
        }
    }
}
