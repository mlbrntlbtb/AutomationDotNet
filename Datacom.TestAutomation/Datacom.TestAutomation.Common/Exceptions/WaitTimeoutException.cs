using System.Runtime.Serialization;

namespace Datacom.TestAutomation.Common
{
    public class WaitTimeoutException : Exception
    {
        public WaitTimeoutException() { }
        public WaitTimeoutException(string message) : base(message) { }
        public WaitTimeoutException(string message, Exception innerException) 
            : base(message, innerException) { }

        protected WaitTimeoutException(
            SerializationInfo info,
            StreamingContext context) : base(info, context) { }
    }
}