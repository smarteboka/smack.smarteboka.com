using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using smack.smarteboka.com.Models;
using Common.Publishers.Slack;

namespace smack.smarteboka.com.Controllers
{
    public class SlackController : Controller
    {
        private static SlackPublisher _slackPublisher;

        public SlackController(SlackPublisher slackPublisher)
        {
            _slackPublisher = slackPublisher;
        }

        public async Task<IActionResult> Index(string username, string emoji, string message, string channel)
        {
            await _slackPublisher.PostMessage(username, emoji, message, channel);            
            return Ok();
        } 
    }
}
