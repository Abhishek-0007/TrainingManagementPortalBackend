using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TrainingManagementPortal.Models.RequestViewModels
{
    public class LoginRequestModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        public string Username { get; set; } = String.Empty;

        [Required]
        public string Password { get; set; }
    }
}
