using System;

namespace ReportExtractor.Domain
{
    public sealed class InputException : Exception
    {
        public InputException(string message) : base(message)
        {
        }
    }
}
