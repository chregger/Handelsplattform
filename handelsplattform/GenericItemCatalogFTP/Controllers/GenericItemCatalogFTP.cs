using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Cache;
using Microsoft.AspNetCore.Mvc;

namespace GenericItemCatalogFTP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenericItemCatalogFTP : Controller
    {
        private string[] _genericCatalog = { "Default Item FTP Call failed!" };
        private readonly WebClient _request = new WebClient();
        private readonly string _url = "ftp://ftp29.world4you.com/productlist.txt";  //Danke an Gruppe Paul Z. und Stefan R. für den FTP Testzugang

        [HttpGet]
        public IEnumerable<string> Get()
        {
            var requestPolicy = new HttpRequestCachePolicy(HttpCacheAgeControl.MaxAge, TimeSpan.FromMinutes(10));
            _request.Credentials = new NetworkCredential("stefanraming_ieg", "2mda.xUdtPigTWJabs@_"); //Danke an Gruppe Paul Z. und Stefan R. für den FTP Testzugang
            _request.CachePolicy = requestPolicy;
            try
            {
                var newFileData = _request.DownloadData(_url);
                var fileString = System.Text.Encoding.UTF8.GetString(newFileData);
                _genericCatalog = fileString.Split(",");

                Console.WriteLine("---- Console Test ----");
                Console.WriteLine(fileString);

                return _genericCatalog;
            }
            catch (WebException)
            {
                // maybe logging
                return new string[] { "Error" };
            }
           
        }

    }
}
