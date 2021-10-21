using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using BookAPI.Models;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using System.Text;
using Newtonsoft.Json.Linq;
using JWT;
using JWT.Algorithms;
using JWT.Serializers;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Http;

namespace BookAPI.Controllers
{
    public class BooksController : Controller
    {
        static readonly log4net.ILog _log4net = log4net.LogManager.GetLogger(typeof(BooksController));
        IConfiguration _config;
        //  private readonly JwtTokenExp tokenclass;
        //code added
        private IJsonSerializer _serializer = new JsonNetSerializer();
        private IDateTimeProvider _provider = new UtcDateTimeProvider();
        private IBase64UrlEncoder _urlEncoder = new JwtBase64UrlEncoder();
        private IJwtAlgorithm _algorithm = new HMACSHA256Algorithm();
        public BooksController(IConfiguration config)
        {

            _config = config;
        }
        [HttpGet]

        public ActionResult Login()
        {
            //HttpContext.Response.Cookies.Delete("Token");
            return View();

        }
        /// <summary>
        /// This method creates the View for Authorized Users
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Login(MemberLogin obj)
        {
            _log4net.Info("User" + obj.Name + " is Logging in");

            string TokenForLogin;
            string data = JsonConvert.SerializeObject(obj);

            try
            {
                Client obj1 = new Client();
                HttpClient client = obj1.Authapi();

                TokenForLogin = GetToken(client, obj);

                if (!string.IsNullOrEmpty(TokenForLogin))
                {

                    HttpContext.Response.Cookies.Append("Token", TokenForLogin);
                    //my added code below  

                    IJwtValidator _validator = new JwtValidator(_serializer, _provider);
                    IJwtDecoder decoder = new JwtDecoder(_serializer, _validator, _urlEncoder, _algorithm);
                    var tokenExp = decoder.DecodeToObject<JwtTokenExp>(TokenForLogin);
                    DateTimeOffset dateTimeOffset = DateTimeOffset.FromUnixTimeSeconds(tokenExp.exp);
                    DateTime timeExp = dateTimeOffset.LocalDateTime;

                    HttpContext.Response.Cookies.Append("Expiry", timeExp.ToString());
                    //my addedcode above
                    HttpContext.Session.SetString("Username", obj.Name);
                    return RedirectToAction("Indes");
                }
                ViewBag.Message = "Invalid Username or Password";
                return View("Login");
            }
            catch (Exception ex)
            {
                return View("Error", ex);
            }
        }
        public ActionResult Indes()
        {
            _log4net.Info("User is Entering in to categories page");

            string Token = HttpContext.Request.Cookies["Token"];
            if (string.IsNullOrEmpty(Token))
            {
                return View("Login");
            }
            return View();
        }
        static string GetToken(HttpClient client, MemberLogin user)
        {

            var json = JsonConvert.SerializeObject(user);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var response = client.PostAsync("/api/Auth", data).Result;
            if (response.IsSuccessStatusCode)
            {
                string token = response.Content.ReadAsStringAsync().Result;
                return token;
            }

            return null;

        }
        public IActionResult Logout()
        {
            _log4net.Info("User is Logging out");

            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }

        [HttpGet]
        public ActionResult RegisterUsers()
        {

            return View();
        }
        [HttpPost]
        public async Task<ActionResult> RegisterUsers(Login e)
        {
            _log4net.Info("New User is" + e.LoginId + "Registering");

            Login obj = new Login();
            using (var httpClient = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(e), Encoding.UTF8, "application/json");

                using (var response = await httpClient.PostAsync("https://localhost:44382/api/Reg/", content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                }
            }
            return RedirectToAction("Login");
        }
    


string Baseurl = "https://localhost:44392/";
        public async Task<ActionResult> Index()
        {
            _log4net.Info("User is Shoppiing Books");


            string Token = HttpContext.Request.Cookies["Token"];
            if (string.IsNullOrEmpty(Token))
            {
                return View("Login");
            }
            else
            {
                List<Book> BookDetail = new List<Book>();

                using (var client = new HttpClient())
                {
                    //Passing service base url  
                    client.BaseAddress = new Uri(Baseurl);

                    client.DefaultRequestHeaders.Clear();
                    //Define request data format  
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    //Sending request to find web api REST service resource GetAllEmployees using HttpClient  
                    HttpResponseMessage Res = await client.GetAsync("https://localhost:44392/api/Books/");

                    //Checking the response is successful or not which is sent using HttpClient  
                    if (Res.IsSuccessStatusCode)
                    {
                        //Storing the response details recieved from web api   
                        var BookResponse = Res.Content.ReadAsStringAsync().Result;

                        //Deserializing the response recieved from web api and storing into the Employee list  
                        BookDetail = JsonConvert.DeserializeObject<List<Book>>(BookResponse);

                    }
                    //returning the employee list to view  
                    return View(BookDetail);
                }
            }
        }
        [HttpGet]
        public ActionResult AddBook()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> AddBook(Book e)
        {
            Book Bookobj = new Book();
            using (var httpClient = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(e), Encoding.UTF8, "application/json");

                using (var response = await httpClient.PostAsync("https://localhost:44392/api/Books/", content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                   // Bookobj = JsonConvert.DeserializeObject<Book>(apiResponse);
                }
            }
            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<ActionResult> Edit(int id)
        {
            _log4net.Info("Get Product by " + id + " is invoked");

            Book obj = new Book();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:44392/api/Books/" + id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    obj = JsonConvert.DeserializeObject<Book>(apiResponse);
                }
            }
            return View(obj);
        }
        [HttpPost]
        public async Task<ActionResult> Edit(Book obj)
        {
            Book receivedemp = new Book();

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
                int id = obj.Id;
                StringContent content1 = new StringContent(JsonConvert.SerializeObject(obj), Encoding.UTF8, "application/json");
                using (var response = await httpClient.PutAsync("https://localhost:44392/api/Books/" + id, content1))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    ViewBag.Result = "Success";
                   // receivedemp = JsonConvert.DeserializeObject<Book>(apiResponse);
                }
            }
            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<ActionResult> Delete(int id)
        {
            _log4net.Info("Get Product by " + id + " is Deleted");

            Book e = new Book();
            e.Id = id;

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:44392/api/Books/" + id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    e = JsonConvert.DeserializeObject<Book>(apiResponse);
                }
            }
            return View(e);
        }


        [HttpPost]
        // [ActionName("DeleteEmployee")]
        public async Task<ActionResult> Delete(Book e)
        {
            int Bookid = e.Id;
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.DeleteAsync("https://localhost:44392/api/Books/" + Bookid))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                }
            }

            return RedirectToAction("Index");
        }
        
        public async Task<ActionResult> DetailItems(int id)
        {

            Book e = new Book();
            _log4net.Info("Details of book " + e.Name + " is invoked");

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:44392/api/Books/" + id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    e = JsonConvert.DeserializeObject<Book>(apiResponse);
                }
            }
            return View(e);
        }
       

        [HttpGet]
        public ActionResult ShipmentDet()
        {
             return View();
        }
        [HttpPost]
        public async Task<ActionResult> ShipmentDet(ShipmentDetail e)
        {
            _log4net.Info("User " + e.BuyersName + "Product Shipped");

            ShipmentDetail obj = new ShipmentDetail();
            using (var httpClient = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(e), Encoding.UTF8, "application/json");

                using (var response = await httpClient.PostAsync("https://localhost:44352/api/Shipment/", content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    // Bookobj = JsonConvert.DeserializeObject<Book>(apiResponse);
                }
            }
            return RedirectToAction("PaymentsMode");
        }
        public IActionResult PaymentsMode()
        {
            _log4net.Info("User is entering payment mode");

            return View();
        }
        public IActionResult Thanks()
        {
            _log4net.Info("User is entering thank you page");

            return View();
        }

    }
}
