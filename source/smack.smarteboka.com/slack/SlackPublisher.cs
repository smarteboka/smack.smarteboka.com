using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging;
using smack.smarteboka.com.Models;

namespace Common.Publishers.Slack
{
    public class SlackPublisher 
    {
        private readonly Uri _uri; 
        private static readonly HttpClient HttpClient = new HttpClient();
        private readonly ILogger<SlackPublisher> _logger;


        public SlackPublisher(IOptions<SlackOptions> slackOptions, ILogger<SlackPublisher> logger)
        {
            _uri = new Uri(slackOptions.Value.WebhookUrl);            
            _logger = logger;
        }

        public async Task PostMessage(SlackModel model)
        {
            var payload = new SlackPayload
            {
                Channel = model.Channel,
                Username = model.Username,
                Emojii = model.Emoji,
                mrkdwn = true,
                Text = model.Message,
            };

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
