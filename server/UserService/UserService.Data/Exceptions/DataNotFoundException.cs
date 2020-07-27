using System;

namespace UserService.Data.Exceptions
{
    public class DataNotFoundException : Exception
    {
        public DataNotFoundException()
        {

        }
        public DataNotFoundException(string errorMessage) : base(errorMessage)
        {

        }
    }
}
