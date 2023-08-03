using Clinet.Models.DTO;
using Clinet.Services.Interfaces;

namespace Clinet.Services
{
    public class BookService : GenericService<BookDTO>, IBookService
    {
        public BookService(IHttpClientFactory _clientFactory, IConfiguration configuration) 
            : base("/api/Book", _clientFactory, configuration)
        {
        }
    }
}
