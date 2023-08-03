using System.Net;

namespace Infrastructure.CustomException
{
    public class BadRequestException : Exception
    {
        // Status code property
        public HttpStatusCode StatusCode { get; set; } = HttpStatusCode.BadRequest;

        public BadRequestException(string message)
            : base(message)
        {
        }

        public BadRequestException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

    }
}
