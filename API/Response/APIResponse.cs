using System.Net;

namespace API.Response
{
    /// <summary>
    /// Generic API Response
    /// </summary>
    public class APIResponse
    {
        /// <summary>
        /// empty Ctor
        /// </summary>
        public APIResponse()
        {                
        }
        /// <summary>
        /// if give result assigned
        /// </summary>
        public APIResponse(object res)
        {
            Result = res;
        }

        /// <summary>
        /// Status Code
        /// </summary>
        public HttpStatusCode StatusCode { get; set; } = HttpStatusCode.OK;

        /// <summary>
        /// If it is success is True Otherwise False
        /// </summary>
        public bool IsSuccess { get; set; } = true;

        /// <summary>
        /// List of error messages
        /// </summary>
        public List<string> ErrorMessages { get; set; } = null!;

        /// <summary>
        /// Any nesessary info which need to be sent
        /// </summary>
        public object Result { get; set; } = null!;

    }
}
