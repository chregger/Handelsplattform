using LoggingService.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;

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

        /// <summary>
        /// Get accepted creditcards by robin mode
        /// If response fails 1 sec will be waited and retry again
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<string> Get()
        {

            List<string> cards = new List<string> { "error" };
            //base adress of the creditcardservice
            serviceBaseAddress = "https://handelsplattformiegeasycreditcardservice.azurewebsites.net/api/AcceptedCreditCards";
            int retrycount = 0;
            HttpResponseMessage response;

            //for loop for the retry mode
            //robin mode -> try till response is succesful 
            //if retrymax is reached robin count will be increased
            for (; retrycount <= retrymax; retrycount++)
            {
                serviceBaseAddress = "https://handelsplattformiegeasycreditcardservice.azurewebsites.net/api/AcceptedCreditCards";

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
                    //Add entry in logging service
                    logging("Error on Service call: " + response.StatusCode + "from Service " + robin);
                    //wait 1 sec till the next try starts
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


        /// <summary>
        /// Logging function to add an entry to the logging microservice
        /// </summary>
        /// <param name="log"></param>
        private void logging(string log)
        {
            var httpWebRequest = (HttpWebRequest)WebRequest.Create("https://handelsplattformlogging.azurewebsites.net/api/Logging");
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
