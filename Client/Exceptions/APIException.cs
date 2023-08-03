namespace Client.Exceptions
{
    public class APIException : Exception
    {
        public string ErrorMessage { get; set; }

        public APIException(string? message) : base(message) => ErrorMessage = message ?? "Unknown Exception";
    }
}
