using System.ComponentModel.DataAnnotations;


namespace UserService.Api.DTO
{
    public class EmailVerificationDTO
    {
        [EmailAddress]
        public string Email { get; set; }
    }
}
