using System.ComponentModel.DataAnnotations;


namespace UserService.Api.DTO
{
    public class LoginDTO
    {
        [EmailAddress]
        [Required]
        public string Email { get; set; }
        [MinLength(6)]
        [Required]
        public string Password { get; set; }

    }
}
