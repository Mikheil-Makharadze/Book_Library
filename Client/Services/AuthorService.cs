using Clinet.Models.DTO;
using Clinet.Services.Interfaces;

namespace Clinet.Services
{
    public class AuthorService : GenericService<AuthorDTO>, IAuthorService
    {
        protected static string Url = "/api/Author";
        public AuthorService(IHttpClientFactory _clientFactory, IConfiguration configuration) : base(Url, _clientFactory, configuration)
        {
        }
    }
}
