﻿using System;
using System.Collections.Generic;
using System.Text;

namespace UserService.Data.Exceptions
{
    public class UserNotFoundException : Exception
    {
        public UserNotFoundException()
        {

        }
        public UserNotFoundException(Guid userId) : base($"User with id:{userId} was not found")
        {

        }
    }
}