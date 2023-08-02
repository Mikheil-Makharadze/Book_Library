using Core.Entities;

namespace Core.Interface
{
    public interface IAuthorService : IGenericRepository<Author>
    {
        Task<IEnumerable<Author>> GetAllDetailsAsync(string? search);
        Task<Author> GetByIdDetailsAsync(int id);
    }
}
