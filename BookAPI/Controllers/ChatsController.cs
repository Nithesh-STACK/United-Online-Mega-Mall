using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using BookAPI.Models;
using Microsoft.AspNetCore.Mvc;
using MVC_Assign.Models;
using Newtonsoft.Json;

namespace BookAPI.Controllers
{
    public class ChatsController : Controller
    {
        //string Baseurl = "https://localhost:44391/";
        static readonly log4net.ILog _log4net = log4net.LogManager.GetLogger(typeof(ChatsController));

        public async Task<ActionResult> Chaat()
        {
            List<Chaat> Info = new List<Chaat>();

            using (var client = new HttpClient())
            {
                //Passing service base url  
                // client.BaseAddress = new Uri(Baseurl);

                client.DefaultRequestHeaders.Clear();
                //Define request data format  
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //Sending request to find web api REST service resource GetAllEmployees using HttpClient  
                HttpResponseMessage Res = await client.GetAsync("https://localhost:44391/api/Menu/");

                //Checking the response is successful or not which is sent using HttpClient  
                if (Res.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api   
                    var Response = Res.Content.ReadAsStringAsync().Result;

                    //Deserializing the response recieved from web api and storing into the Employee list  
                    Info = JsonConvert.DeserializeObject<List<Chaat>>(Response);

                }
                //returning the employee list to view  
                return View(Info);
            }
        }

        public async Task<ActionResult> Drinks()
        {
            List<Drink> Info = new List<Drink>();

            using (var client = new HttpClient())
            {
                //Passing service base url  
                // client.BaseAddress = new Uri(Baseurl);

                client.DefaultRequestHeaders.Clear();
                //Define request data format  
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //Sending request to find web api REST service resource GetAllEmployees using HttpClient  
                HttpResponseMessage Res = await client.GetAsync("https://localhost:44391/api/Menu/drinks/");

                //Checking the response is successful or not which is sent using HttpClient  
                if (Res.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api   
                    var Response = Res.Content.ReadAsStringAsync().Result;

                    //Deserializing the response recieved from web api and storing into the Employee list  
                    Info = JsonConvert.DeserializeObject<List<Drink>>(Response);
                }
                //returning the employee list to view  
                return View(Info);
            }
        }

        public async Task<ActionResult> Edit(int id)
        {
            Chaat ch = new Chaat();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:44391/api/Menu/" + id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    ch = JsonConvert.DeserializeObject<Chaat>(apiResponse);
                }
            }
            return View(ch);


        }
        [HttpPost]
        public async Task<ActionResult> Edit(Chaat c)
        {
            Chaat receivedemp = new Chaat();

            using (var httpClient = new HttpClient())
            {
                #region
                //var content = new MultipartFormDataContent();
                //content.Add(new StringContent(reservation.Empid.ToString()), "Empid");
                //content.Add(new StringContent(reservation.Name), "Name");
                //content.Add(new StringContent(reservation.Gender), "Gender");
                //content.Add(new StringContent(reservation.Newcity), "Newcity");
                //content.Add(new StringContent(reservation.Deptid.ToString()), "Deptid");
                #endregion
                int id = c.Cid;
                StringContent content1 = new StringContent(JsonConvert.SerializeObject(c), Encoding.UTF8, "application/json");
                using (var response = await httpClient.PutAsync("https://localhost:44391/api/Menu/" + id, content1))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    ViewBag.Result = "Success";
                    receivedemp = JsonConvert.DeserializeObject<Chaat>(apiResponse);
                }
            }
            return RedirectToAction("Chaat");
        }
        public async Task<ActionResult> DetailItems(int id)
        {
            Chaat e = new Chaat();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:44391/api/Menu/" + id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    e = JsonConvert.DeserializeObject<Chaat>(apiResponse);
                }
            }
            return View(e);
        }


    }
}

   
