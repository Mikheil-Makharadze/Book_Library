using Clinet.Models.DTO;
using Clinet.Services.Interfaces;

namespace Clinet.Services
{
    public class BookService : GenericService<BookDTO>, IBookService
    {
        protected static string Url = "/api/Book";
        public BookService(IHttpClientFactory _clientFactory, IConfiguration configuration) : base(Url, _clientFactory, configuration)
        {
        }
    }
}
