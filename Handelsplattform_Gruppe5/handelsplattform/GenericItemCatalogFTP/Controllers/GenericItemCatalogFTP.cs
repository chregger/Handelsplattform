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
        string[] genericcatalog = new string[] { "Default Item FTP Call failed!" };
        WebClient request = new WebClient();
        string url = "ftp://ftp29.world4you.com/productlist.txt";  //Danke an Gruppe Paul Z. und Stefan R. für den FTP Testzugang

        [HttpGet]
        public IEnumerable<string> Get()
        {
            HttpRequestCachePolicy requestPolicy = new HttpRequestCachePolicy(HttpCacheAgeControl.MaxAge, TimeSpan.FromMinutes(10));
            request.Credentials = new NetworkCredential("stefanraming_ieg", "2mda.xUdtPigTWJabs@_"); //Danke an Gruppe Paul Z. und Stefan R. für den FTP Testzugang
            request.CachePolicy = requestPolicy;

            try
            {
                byte[] newFileData = request.DownloadData(url);
                string fileString = System.Text.Encoding.UTF8.GetString(newFileData);
                genericcatalog = fileString.Split(",");

                Console.WriteLine("---- Console Test ----");
                Console.WriteLine(fileString);

                return genericcatalog;
            }
            catch (WebException e)
            {
                // maybe logging
                return new string[] { "Error" };
            }
           
        }

    }
}
