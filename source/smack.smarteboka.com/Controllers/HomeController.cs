using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Common.Publishers.Slack;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using smack.smarteboka.com.Models;

namespace smack.smarteboka.com.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private static SlackPublisher _slackPublisher;
        private readonly ILogger<HomeController> _logger;

        public HomeController(SlackPublisher slackPublisher, ILogger<HomeController> logger)
        {
            _slackPublisher = slackPublisher;
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public async Task<IActionResult> Publish(SlackModel slackModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _slackPublisher.PostMessage(slackModel);
                    TempData["success_message"] = "Sent!";
                }
                catch (Exception e)
                {
                    _logger.LogError(e.ToString());
                    TempData["error_message"] = e.ToString();
                }

                return View("Index", slackModel);
            }
            else
            {
                TempData["error_message"] = "Plz fill all fields, Smarting";
                return View("Index", slackModel);
            }
        }
    }
}
