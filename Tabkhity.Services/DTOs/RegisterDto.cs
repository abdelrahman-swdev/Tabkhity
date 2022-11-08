using System.ComponentModel.DataAnnotations;

namespace Tabkhity.Services.DTOs
{
    public class RegisterDto
    {
        [Required]
        public string Name { get; set; }
        [Required, EmailAddress]
        public string Email { get; set; }

        [Required]
        [RegularExpression("(?=^.{8,10}$)(?=.*\\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[!@#$%^&amp;*()_+}{&quot;:;'?/&gt;.&lt;,])(?!.*\\s).*$",
        ErrorMessage = "Password must have 1 uppercase, 1 lowercase, 1 number, 1 nonalphanumeric and atleast 8 characters")]
        public string Password { get; set; }

        [Required]
        [RegularExpression("(?=^.{8,10}$)(?=.*\\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[!@#$%^&amp;*()_+}{&quot;:;'?/&gt;.&lt;,])(?!.*\\s).*$",
        ErrorMessage = "Password must have 1 uppercase, 1 lowercase, 1 number, 1 nonalphanumeric and atleast 8 characters")]
        [Compare(nameof(Password))]
        public string ConfirmPassword { get; set; }
    }
}
