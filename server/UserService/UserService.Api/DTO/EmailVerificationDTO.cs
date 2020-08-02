using System.ComponentModel.DataAnnotations;


namespace UserService.Api.DTO
{
    public class EmailVerificationDTO
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
