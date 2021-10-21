using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using BookAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace BookAPI.Controllers
{
    public class CartController : Controller
    {
        string Baseurl = "https://localhost:44344/";

        static readonly log4net.ILog _log4net = log4net.LogManager.GetLogger(typeof(CartController));

        public async Task<ActionResult> CartItems()
        {
            _log4net.Info("user is entering cart items of books");

            string Token = HttpContext.Request.Cookies["Token"];

            List<AddCart> cart = new List<AddCart>();

            using (var client = new HttpClient())
            {
                //Passing service base url  
                client.BaseAddress = new Uri(Baseurl);

                client.DefaultRequestHeaders.Clear();
                //Define request data format  
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //Sending request to find web api REST service resource GetAllEmployees using HttpClient  
                HttpResponseMessage Res = await client.GetAsync("https://localhost:44344/api/Cart/");

                //Checking the response is successful or not which is sent using HttpClient  
                if (Res.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api   
                    var CartResponse = Res.Content.ReadAsStringAsync().Result;

                    //Deserializing the response recieved from web api and storing into the Employee list  
                    cart = JsonConvert.DeserializeObject<List<AddCart>>(CartResponse);

                }
                //returning the employee list to view  
                return View(cart);
            }
        }
        //[HttpGet]
        //public async Task<ActionResult> AddCart(int id)
        //{
        //    _log4net.Info(" Product is" + id+" added to cart");

        //    Book obj = new Book();
        //    using (var httpClient = new HttpClient())
        //    {
        //        using (var response = await httpClient.GetAsync("https://localhost:44392/api/Books/" + id))
        //        {
        //            string apiResponse = await response.Content.ReadAsStringAsync();
        //            obj = JsonConvert.DeserializeObject<Book>(apiResponse);
        //        }
        //    }
        //    return View(obj);
        //}
        //[HttpPost]
        public async Task<ActionResult> AddCart(Book e)
        {
            _log4net.Info("user is adding" +e.Name+"to cart");

            Book Bookobj = new Book();
            AddCart crt = new AddCart();
            crt.Id = e.Id;
            crt.Name = e.Name;
            crt.Photo = e.Photo;
            crt.Category = e.Category;
            crt.PlotDescription = e.PlotDescription;
            crt.Price = e.Price;
            using (var httpClient = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(crt), Encoding.UTF8, "application/json");

                using (var response = await httpClient.PostAsync("https://localhost:44344/api/Cart/", content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    // Bookobj = JsonConvert.DeserializeObject<Book>(apiResponse);
                }
            }
            return RedirectToAction("CartItems");
        }
        [HttpGet]
        public async Task<ActionResult> DeleteCart(int id)
        {
            _log4net.Info(" Product is" + id + " removed from cart");

            AddCart e = new AddCart();
            e.Id = id;

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:44344/api/Cart/" + id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    e = JsonConvert.DeserializeObject<AddCart>(apiResponse);
                }
            }
            return View(e);
        }


        [HttpPost]
        public async Task<ActionResult> DeleteCart(AddCart e)
        {
            _log4net.Info(e.Name + "removed from cart");

            int Bookid = e.Id;
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.DeleteAsync("https://localhost:44344/api/Cart/" + Bookid))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                }
            }

            return RedirectToAction("CartItems");
        }
        //[HttpGet]
        //public async Task<ActionResult> ProductCart(int id)
        //{
        //   Pro obj = new Pro();
        //    using (var httpClient = new HttpClient())
        //    {
        //        using (var response = await httpClient.GetAsync("https://localhost:44387/api/ProductsBooking/" + id))
        //        {
        //            string apiResponse = await response.Content.ReadAsStringAsync();
        //            obj = JsonConvert.DeserializeObject<Pro>(apiResponse);
        //        }
        //    }
        //    return View(obj);
        //}
        //[HttpPost]
        public async Task<ActionResult> ProductCart(ProductsCart e)
        {
            _log4net.Info("user is adding "+e.Productname+"items of product");

            Pro Bookobj = new Pro();
            ProductsCart pr = new ProductsCart();
            pr.Productid = e.Productid;
            pr.ProductImage = e.ProductImage;
            pr.Productname = e.Productname;
            pr.Productprice = e.Productprice;
            using (var httpClient = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(pr), Encoding.UTF8, "application/json");

                using (var response = await httpClient.PostAsync("https://localhost:44344/api/Product/", content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    // Bookobj = JsonConvert.DeserializeObject<Book>(apiResponse);
                }
            }
            return RedirectToAction("ProductItems");
        }
        public async Task<ActionResult> ProductItems()
        {
            string Token = HttpContext.Request.Cookies["Token"];

            List<ProductsCart> cart = new List<ProductsCart>();

            using (var client = new HttpClient())
            {
                //Passing service base url  
                client.BaseAddress = new Uri(Baseurl);

                client.DefaultRequestHeaders.Clear();
                //Define request data format  
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //Sending request to find web api REST service resource GetAllEmployees using HttpClient  
                HttpResponseMessage Res = await client.GetAsync("https://localhost:44344/api/Product/");

                //Checking the response is successful or not which is sent using HttpClient  
                if (Res.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api   
                    var CartResponse = Res.Content.ReadAsStringAsync().Result;

                    //Deserializing the response recieved from web api and storing into the Employee list  
                    cart = JsonConvert.DeserializeObject<List<ProductsCart>>(CartResponse);

                }
                //returning the employee list to view  
                return View(cart);
            }
        }
        //[HttpGet]
        public async Task<ActionResult> DeleteProCart(int id)
        {

            ProductsCart e = new ProductsCart();

            e.Productid = id;
            _log4net.Info("user is "+e.Productname+" is deleted from cart");

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.DeleteAsync("https://localhost:44344/api/Product/" + id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    //e = JsonConvert.DeserializeObject<ProductsCart>(apiResponse);
                }
            }
            return RedirectToAction("ProductItems");
        }


        //[HttpPost]
        //// [ActionName("DeleteEmployee")]
        //public async Task<ActionResult> DeleteProCart(ProductsCart e)
        //{
        //    int Bookid = e.Productid;
        //    using (var httpClient = new HttpClient())
        //    {
        //        using (var response = await httpClient.DeleteAsync("https://localhost:44344/api/Product/" + Bookid))
        //        {
        //            string apiResponse = await response.Content.ReadAsStringAsync();
        //        }
        //    }

        //    return RedirectToAction("ProductItems");
        //}
        //[HttpGet]
        //public async Task<ActionResult> ChaatCart(int id)
        //{
        //    Chaat obj = new Chaat();
        //    using (var httpClient = new HttpClient())
        //    {
        //        using (var response = await httpClient.GetAsync("https://localhost:44391/api/Menu/" + id))
        //        {
        //            string apiResponse = await response.Content.ReadAsStringAsync();
        //            obj = JsonConvert.DeserializeObject<Chaat>(apiResponse);
        //        }
        //    }
        //    return View(obj);
        //}
        //[HttpPost]
        public async Task<ActionResult> ChaatsCart(Chaat e)
        {
            _log4net.Info("user is adding"+e.Cname+" item of chaat");

            Chaat Bookobj = new Chaat();
            ChaatsCart car = new ChaatsCart();
            car.Cid = e.Cid;
            car.Cimage = e.Cimage;
            car.Cname = e.Cname;
            car.Cprice = e.Cprice;
            using (var httpClient = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(car), Encoding.UTF8, "application/json");

                using (var response = await httpClient.PostAsync("https://localhost:44344/api/Chaat/", content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    // Bookobj = JsonConvert.DeserializeObject<Book>(apiResponse);
                }
            }
            return RedirectToAction("ChaatItems");
        }
        public async Task<ActionResult> ChaatItems()
        {
            _log4net.Info("user is shopping item of chaat");

            string Token = HttpContext.Request.Cookies["Token"];

            List<ChaatsCart> cart = new List<ChaatsCart>();

            using (var client = new HttpClient())
            {
                //Passing service base url  
                client.BaseAddress = new Uri(Baseurl);

                client.DefaultRequestHeaders.Clear();
                //Define request data format  
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //Sending request to find web api REST service resource GetAllEmployees using HttpClient  
                HttpResponseMessage Res = await client.GetAsync("https://localhost:44344/api/Chaat/");

                //Checking the response is successful or not which is sent using HttpClient  
                if (Res.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api   
                    var CartResponse = Res.Content.ReadAsStringAsync().Result;

                    //Deserializing the response recieved from web api and storing into the Employee list  
                    cart = JsonConvert.DeserializeObject<List<ChaatsCart>>(CartResponse);

                }
                //returning the employee list to view  
                return View(cart);
            }
        }
        //[HttpGet]
        public async Task<ActionResult> DeleteCarts(int id)
        {
            ChaatsCart b = new ChaatsCart();
            b.Cid = id;
            _log4net.Info("user is deleting" + b.Cname + " item of chaat");

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.DeleteAsync("https://localhost:44344/api/Chaat/" + id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    //b = JsonConvert.DeserializeObject<ChaatsCart>(apiResponse);
                }
            }
            return RedirectToAction("ChaatItems");
        }
        //[HttpPost]
        //public async Task<ActionResult> DeleteCarts(ChaatsCart b)
        //{
        //    int chatid = b.Cid;
        //    using (var httpClient = new HttpClient())
        //    {
        //        using (var response = await httpClient.DeleteAsync("https://localhost:44344/api/Chaat/" +chatid))
        //        {
        //            string apiResponse = await response.Content.ReadAsStringAsync();
        //        }
        //    }

        //    return RedirectToAction("ChaatItems");
        //}

    }
}
