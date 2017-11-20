using Common.Publishers.Slack;
using Newtonsoft.Json;

namespace Common.Publishers
{
    public class SlackPayload
    {
        public SlackPayload()
        {
            Attachments = new SlackAttachment[0];
        }
        
        [JsonProperty("text")]
        public string Text { get; set; }

        [JsonProperty("channel")]
        public string Channel { get; set; }

        [JsonProperty("username")]
        public string Username { get; set; }

        [JsonProperty("icon_emoji")]
        public string Emojii { get; set; }

        [JsonProperty("attachments")]
        public SlackAttachment[] Attachments { get; set; }

        [JsonProperty("mrkdwn")]
        public bool mrkdwn { get; set; }        
    }
}