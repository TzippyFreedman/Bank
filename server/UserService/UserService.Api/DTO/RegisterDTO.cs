using System.ComponentModel.DataAnnotations;


namespace UserService.Api.DTO
{
    public class RegisterDTO
    {
        [Required]
        [StringLength(4)]
        public string VerificationCode { get; set; }
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MinLength(6)]
        public string Password { get; set; }

    }
}
