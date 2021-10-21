using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using BookingClient.Models;
using Newtonsoft.Json;
using System.Text;

namespace BookingClient.Controllers
{
    public class BookingController : Controller
    {
        static readonly log4net.ILog _log4net = log4net.LogManager.GetLogger(typeof(BookingController));

        string Baseurl = "https://localhost:44303/";
        public async Task<ActionResult> Index()
        {
            _log4net.Info("Movies category are invoked");

            List<APIClients> MovieInfo = new List<APIClients>();

            using (var client = new HttpClient())
            {
                //Passing service base url  
                client.BaseAddress = new Uri(Baseurl);

                client.DefaultRequestHeaders.Clear();
                //Define request data format  
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //Sending request to find web api REST service resource GetAllEmployees using HttpClient  
                HttpResponseMessage Res = await client.GetAsync("https://localhost:44303/api/Booking/");

                //Checking the response is successful or not which is sent using HttpClient  
                if (Res.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api   
                    var APIClientResponse = Res.Content.ReadAsStringAsync().Result;

                    //Deserializing the response recieved from web api and storing into the Employee list  
                    MovieInfo = JsonConvert.DeserializeObject<List<APIClients>>(APIClientResponse);

                }
                //returning the employee list to view  
                return View(MovieInfo);
            }
        }
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(APIClients e)
        {
            APIClients Emplobj = new APIClients();
            using (var httpClient = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(e), Encoding.UTF8, "application/json");

                using (var response = await httpClient.PostAsync("https://localhost:44303/api/Booking/", content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    // Emplobj = JsonConvert.DeserializeObject<Emp>(apiResponse);
                }
            }
            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<ActionResult> Edit(int id)
        {
            APIClients emp = new APIClients();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:44303/api/Booking/" + id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    //emp = JsonConvert.DeserializeObject<APIClient>(apiResponse);
                }
            }
            return View(emp);
        }
        
        [HttpPost]
        public async Task<ActionResult> Edit(APIClients e)
        {
            APIClients receivedemp = new APIClients();

            using (var httpClient = new HttpClient())
            {
               
                int id = e.Id;
                StringContent content1 = new StringContent(JsonConvert.SerializeObject(e), Encoding.UTF8, "application/json");
                using (var response = await httpClient.PutAsync("https://localhost:44302/api/employeenews/" + id, content1))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    ViewBag.Result = "Success";
                   // receivedemp = JsonConvert.DeserializeObject<APIClient>(apiResponse);
                }
            }
            return RedirectToAction("index");
        }


    }
}
