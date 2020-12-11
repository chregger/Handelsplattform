using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Cache;
using System.Threading.Tasks;

namespace ProductCatalogFTP.Controllers
{
    [Route("api/FTP")]
    [ApiController]
    public class ProductCatalog : Controller
    {
        private readonly WebClient _request = new WebClient();
        private readonly string _url = "ftp://192.168.0.176/Test.txt";

        [HttpGet]
        public IEnumerable<string> Get()
        {
            var requestPolicy = new HttpRequestCachePolicy(HttpCacheAgeControl.MaxAge, TimeSpan.FromMinutes(10));
            _request.Credentials = new NetworkCredential("ftp_user", "campus02");
            _request.CachePolicy = requestPolicy;
            try
            {
                byte[] newFileData = _request.DownloadData(_url);
                string fileString = System.Text.Encoding.UTF8.GetString(newFileData);

                Console.WriteLine("---- Console Test ----");
                Console.WriteLine(fileString);

                return fileString.Split(",");
            }
            catch (WebException)
            {
                return new string[] { "Error" };
            }

        }
    }
}
