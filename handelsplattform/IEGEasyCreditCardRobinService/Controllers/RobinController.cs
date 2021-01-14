using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using IEGEasyCreditCardRobinService.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace IEGEasyCreditCardRobinService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RobinController : Controller
    {
        private int _robin = 1;
        private const int RetryMax = 5;
        private string _serviceBaseAddress;
        private static readonly HttpClient Client = new HttpClient();

        /// <summary>
        /// Get accepted credit cards by robin mode
        /// If response fails 1 sec will be waited and retry again
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            var cards = new List<string> { "error" };
            //base address of the credit card service
            _serviceBaseAddress = "https://handelsplattformiegeasycreditcardservice.azurewebsites.net/api/AcceptedCreditCards";
            var retryCount = 0;
            HttpResponseMessage response;

            //for loop for the retry mode
            //robin mode -> try till response is successful
            //if the maximal retries are reached robin count will be increased
            for (; retryCount <= RetryMax; retryCount++)
            {
                _serviceBaseAddress = "https://handelsplattformiegeasycreditcardservice.azurewebsites.net/api/AcceptedCreditCards";

                Client.BaseAddress = new Uri(_serviceBaseAddress);
                Client.DefaultRequestHeaders.Accept.Clear();
                Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                response = Client.GetAsync(_serviceBaseAddress).Result;
                Console.WriteLine("Retry: " + retryCount + " on " + _serviceBaseAddress);

                if (response.IsSuccessStatusCode)
                {
                    cards = response.Content.ReadAsAsync<List<string>>().Result;
                    retryCount = 0;
                    cards.Add("Dummy added: /api/AcceptedCreditCardController call on Service" + _robin + " successful");
                    return cards;
                }

                //Add entry in logging service
                Log("Error on Service call: " + response.StatusCode + "from Service " + _robin);
                
                //wait 1 sec till the next try starts
                System.Threading.Thread.Sleep(1000);
                Console.WriteLine("Sleep");

                if (retryCount != RetryMax)
                {
                    continue;
                }
                retryCount = 0;
                _robin++;
            }
            return cards;
        }


        /// <summary>
        /// Logging function to add an entry to the logging micro-service
        /// </summary>
        /// <param name="log"></param>
        private static void Log(string log)
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
