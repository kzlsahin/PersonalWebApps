using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Mail;
using tracker_webapp.Communication;
using tracker_webapp.Models;

namespace Areas.Tracker.Controllers
{
    [Area("Tracker")]
    public class TrackerHomeController : Controller
    {
        private readonly ILogger<TrackerHomeController> _logger;
        private readonly IEmailSender _mailSender;

        public TrackerHomeController(ILogger<TrackerHomeController> logger, IEmailSender mailSender)
        {
            _logger = logger;
            _mailSender = mailSender;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [HttpPost]
        public JsonResult SendMail(){
            string temp = "<html xmlns=\"http://www.w3.org/1999/xhtml\"> <body><div><h1 style='color: #FF0000;'>Welcome</h1><p>Ben kzlsahin</p></div><body></html>";

            _mailSender.SendEmailAsync("senturk.device1@gmail.com", "Deneme", temp);
            return Json(new { res = true, body = "deniyorum" });
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}