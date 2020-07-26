namespace UserService.Services.Interfaces
{
    public interface ISmtpSettings
    {
        string Address { get; set; }
        string Password { get; set; }
        string SMTPHost { get; set; }
    }
}