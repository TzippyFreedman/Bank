using System;


namespace UserService.Services.Exceptions
{
   public class BadRequestException : Exception
    {
        public BadRequestException()
        {

        }
        public BadRequestException(string errorMessage) : base(errorMessage)
        {

        }
    }
}
