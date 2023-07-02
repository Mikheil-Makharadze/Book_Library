using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interface
{
    public interface IAuthorService : IGenericRepository<Author>
    {
        Task<IEnumerable<Author>> GetAllDetailsAsync(string? search);
        Task<Author> GetByIdDetailsAsync(int id);
    }
}
