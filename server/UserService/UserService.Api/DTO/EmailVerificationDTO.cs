using System.ComponentModel.DataAnnotations;


namespace UserService.Api.DTO
{
    public class EmailVerificationDTO
    {
        [EmailAddress]
        [Required]
        public string Email { get; set; }
    }
}
