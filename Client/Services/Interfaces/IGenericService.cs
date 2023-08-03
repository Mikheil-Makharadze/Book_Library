namespace Clinet.Services.Interfaces
{
    public interface IGenericService<T>
    {
        Task<List<T>> GetAllSelectorItemsAsync(string token);
        Task<List<T>> GetAllAsync(string? searchString, string token);
        Task<T> GetByIdAsync(int id, string token);
        Task AddAsync(object entity, string token);
        Task UpdateAsync(int id, object entity, string token);
        Task DeleteAsync(int id, string token);
    }
}
