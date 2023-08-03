using Clinet.Models.DTO;
using Clinet.Services.Interfaces;

namespace Clinet.Services
{
    public class AuthorService : GenericService<AuthorDTO>, IAuthorService
    {
        public AuthorService(IHttpClientFactory _clientFactory, IConfiguration configuration) 
            : base("/api/Author", _clientFactory, configuration)
        {
        }
    }
}
