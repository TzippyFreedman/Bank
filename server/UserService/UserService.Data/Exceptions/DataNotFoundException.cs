using System;
using System.Collections.Generic;
using System.Text;

namespace UserService.Data.Exceptions
{
    public class DataNotFoundException : Exception
    {
        public DataNotFoundException()
        {

        }
        public DataNotFoundException(string errorMessage): base(errorMessage)
        {
            
        }
    }
}
