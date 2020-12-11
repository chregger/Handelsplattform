using LoggingService.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace IEGEasyCreditCardServiceRobin.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RobinController : Controller
    {
        int robin = 1;
        int retrymax = 5;
        string serviceBaseAddress;
        private static readonly HttpClient client = new HttpClient();

        [HttpGet]
        public IEnumerable<string> Get()
        {

            List<string> cards = new List<string> { "error" };
            serviceBaseAddress = "https://iegeasycreditcardservice20201201092426.azurewebsites.net/api/AcceptedCreditCards";
            int retrycount = 0;
            HttpResponseMessage response;


            for (; retrycount <= retrymax; retrycount++)
            {
                serviceBaseAddress = "https://iegeasycreditcardservice20201201092426.azurewebsites.net/api/AcceptedCreditCards";

                client.BaseAddress = new Uri(serviceBaseAddress);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                response = client.GetAsync(serviceBaseAddress).Result;
                Console.WriteLine("Retry: " + retrycount + " on " + serviceBaseAddress);

                if (response.IsSuccessStatusCode)
                {
                    cards = response.Content.ReadAsAsync<List<string>>().Result;
                    retrycount = 0;
                    cards.Add("Dummy added: /api/AcceptedCreditCardController call on Service" + robin + " successfull");
                    return cards;
                }
                else
                {
                    logging("Error on Service call: " + response.StatusCode + "from Service " + robin);
                    System.Threading.Thread.Sleep(1000);
                    Console.WriteLine("Sleep");

                    if (retrycount == retrymax)
                    {
                        retrycount = 0;
                        robin++;
                    }
                }


            }
            return cards;
        }


        private void logging(string log)
        {
            //straight from stackoverflow

            var httpWebRequest = (HttpWebRequest)WebRequest.Create("https://loggingservice20201201101116.azurewebsites.net/api/Logging");
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";
            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                string json = JsonConvert.SerializeObject(new LogMessage(log));
                streamWriter.Write(json);
            }
            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
            }
        }
    }
}
