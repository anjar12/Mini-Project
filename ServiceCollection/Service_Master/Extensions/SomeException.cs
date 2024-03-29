﻿using System.Globalization;

namespace Service_Master.Extensions
{
    public class SomeException : Exception
    {
        public SomeException() : base()
        {
        }

        public SomeException(string message) : base(message)
        {
        }

        public SomeException(string message, params object[] args)
            : base(String.Format(CultureInfo.CurrentCulture, message, args))
        {
        }
    }
}
