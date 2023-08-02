using Core.Entities;

namespace Core.Interface
{
    public interface IBookService : IGenericRepository<Book>
    {
        Task<IEnumerable<Book>> GetAllDetailsAsync(string? search);
        Task<Book> GetByIdDetailsAsync(int id);
    }
}
