using System;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging;

namespace Common.Publishers.Slack
{
    public class SlackPublisher 
    {
        private readonly Uri _uri; 
        private static readonly HttpClient HttpClient = new HttpClient();
        private ILogger<SlackPublisher> _logger;


        public SlackPublisher(IOptions<SlackOptions> slackOptions, ILogger<SlackPublisher> logger)
        {
            _uri = new Uri(slackOptions.Value.WebhookUrl);            
            _logger = logger;
        }

        public async Task PostMessage(string username, string emoji, string message, string channel, string color = null, Link includeLink = null, SubSection[] subSections = null)
        {
            var payload = new SlackPayload
            {
                Channel = channel,
                Username = username,
                Emojii = emoji,
                mrkdwn = true
            };

            if (subSections == null)
            {
                var slackAttachment = new SlackAttachment
                {
                    text = message,
                    color = color
                };

                if (includeLink != null)
                {
                    slackAttachment.title = includeLink.Title;
                    slackAttachment.title_link = includeLink.Url;
                }
                
                payload.Attachments = new[] { slackAttachment };
            }
            else
            {
                payload.Text = message;
                payload.Attachments = subSections.Select(s => new SlackAttachment { text = s.Text, color = s.Color }).ToArray(); ;
            }

            await PostMessage(payload);
        }

        private async Task PostMessage(SlackPayload slackPayload)
        {
            string payloadJson = JsonConvert.SerializeObject(slackPayload);
            try
            {   
                var res = await HttpClient.PostAsync(_uri, new StringContent(payloadJson, Encoding.UTF8, "application/json"));
                    
                if (!res.IsSuccessStatusCode)
                {
                    _logger.LogInformation(JsonConvert.SerializeObject(res));
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e.ToString());
            }
        }
    }
}
