﻿using UserService.Services.Interfaces;

namespace UserService.Services
{
    public class SmtpSettings 
    {
        public string Address { get; set; }
        public string Password { get; set; }
        public string SMTPHost { get; set; }
    }
}
