using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using BookAPI.Models;
using BookingClient.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace BookAPI.Controllers
{
    public class OnlinePaymentController : Controller
    {
        static readonly log4net.ILog _log4net = log4net.LogManager.GetLogger(typeof(OnlinePaymentController));
        [HttpGet]
        public ActionResult Payment()
        {
            string Token = HttpContext.Request.Cookies["Token"];
            if (string.IsNullOrEmpty(Token))
            {
                return RedirectToAction("Login", "Books");

            }
            else
            {
                return View();
            }
        }
        [HttpPost]
        public async Task<ActionResult> Payment(PaymentDetail e)
        {
            _log4net.Info(" Product payment of" + e.CardHolderName + "Suceessfull");

            PaymentDetail Emplobj = new PaymentDetail();
            using (var httpClient = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(e), Encoding.UTF8, "application/json");

                using (var response = await httpClient.PostAsync("https://localhost:44344/api/Cart/OnlinePay/", content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    // Emplobj = JsonConvert.DeserializeObject<Emp>(apiResponse);
                }
            }
            return RedirectToAction("Thanks");
        }
    }
}
