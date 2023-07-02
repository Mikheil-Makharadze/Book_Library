using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interface
{
    public interface IAuthorBookService
    {
        Task CreateAuthorBook(int authorId, int BookId);
        Task RemoveByAuthorId(int Id);
        Task removeByBookId(int Id);
    }
}
