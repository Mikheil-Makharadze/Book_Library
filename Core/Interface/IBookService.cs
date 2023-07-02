using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interface
{
    public interface IBookService : IGenericRepository<Book>
    {
        Task<IEnumerable<Book>> GetAllDetailsAsync(string? search);
        Task<Book> GetByIdDetailsAsync(int id);
    }
}
