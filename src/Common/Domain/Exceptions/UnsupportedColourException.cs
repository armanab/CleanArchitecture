using System;

namespace CleanApplication.Domain.Exceptions
{
    public class UnsupportedColourException : Exception
    {
        public UnsupportedColourException(string code)
            : base($"Colour \"{code}\" is unsupported.")
        {
        }
    }
}
