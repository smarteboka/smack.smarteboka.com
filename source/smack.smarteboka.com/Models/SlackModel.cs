using System.ComponentModel.DataAnnotations;

namespace smack.smarteboka.com.Models
{
    public class SlackModel
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Emoji { get; set; }
        [Required]
        public string Message { get; set; }
        [Required]
        public string Channel { get; set; }
    }
}