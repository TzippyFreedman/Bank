using System;


namespace UserService.Services.Exceptions
{
   public abstract class BadRequestException : Exception
    {
        public BadRequestException()
        {

        }
        public BadRequestException(string errorMessage) : base(errorMessage)
        {

        }
    }
}
