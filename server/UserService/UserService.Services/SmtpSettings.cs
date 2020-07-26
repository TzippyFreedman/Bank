using System;
using System.Collections.Generic;
using System.Text;
using UserService.Services.Interfaces;

namespace UserService.Services
{
    public class SmtpSettings : ISmtpSettings
    {
        public string Address { get; set; }
        public string Password { get; set; }
        public string SMTPHost { get; set; }
    }
}
