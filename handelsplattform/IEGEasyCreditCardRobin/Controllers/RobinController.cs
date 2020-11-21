using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using IEGEasyCreditCardRobin.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.IO;
using Newtonsoft.Json;

namespace IEGEasyCreditCardRobin.Controllers
{
    [Route("api/RobinController")]
    [ApiController]
    public class RobinController : ControllerBase
    {
        int robin = 1;
        int retrymax = 5;
        int service_count = 2;
        string serviceBaseAddress;
        private static readonly HttpClient client = new HttpClient();

        [HttpGet]
        public IEnumerable<string> Get()
        {

            List<string> cards = new List<string> { "error" };
            //http://ppiegeasycreditcardservice2.azurewebsites.net/api/AcceptedCreditCardsController
            serviceBaseAddress = "http://ppiegeasycreditcardservice" + robin + ".azurewebsites.net";
            int retrycount = 0;
            HttpResponseMessage response;


            for (; retrycount <= retrymax; retrycount++)
            {
                serviceBaseAddress = "http://ppiegeasycreditcardservice" + robin + ".azurewebsites.net";
 
                client.BaseAddress = new Uri(serviceBaseAddress);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                response = client.GetAsync(serviceBaseAddress + "/api/AcceptedCreditCardsController").Result;
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

            var httpWebRequest = (HttpWebRequest)WebRequest.Create("http://ppapplicationlog.azurewebsites.net/api/logcontroller");
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";
            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                string json = JsonConvert.SerializeObject(new Log(log));
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
