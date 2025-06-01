using System.ComponentModel.DataAnnotations;

namespace FAMS.Api.Dtos
{
    public class AuthDto
    {
        [EmailAddress(ErrorMessage = "Email is invalid format.")]
        [Required(ErrorMessage = "Email is empty.")]

        public string? Email { get; set; }
        [Required(ErrorMessage = "Password is empty.")]
        public string? Password { get; set; }
    }
}
