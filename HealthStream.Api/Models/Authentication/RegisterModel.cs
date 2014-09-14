using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace HealthStream.Api.Models.Authentication
{
    public class RegisterModel
    {
        [JsonProperty("username")]
        [Required(ErrorMessage = "Username is required")]
        public string Username { get; set; }

        [JsonProperty("password")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
        
        [JsonProperty("confirm_password")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Password confirmation is required")]
        [Compare("Password", ErrorMessage = "Passwords do not match")]
        public string ConfirmPassword { get; set; }

        [JsonProperty("email_address")]
        [DataType(DataType.EmailAddress)]
        [Required]
        public string EmailAddress { get; set; }
    }
}