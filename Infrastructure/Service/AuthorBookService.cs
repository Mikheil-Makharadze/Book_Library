using Core.Entities;
using Core.Interface;
using Infrastructure.Data.AppDB;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Service
{
    public class AuthorBookService : IAuthorBookService
    {
        private readonly AppDbContext _context;
        public AuthorBookService(AppDbContext context)
        {
            _context = context;
        }
        public async Task CreateAuthorBook(int authorId, int BookId)
        {
            var AuthorBook = new AuthorBook()
            {
                AuthorId = authorId,
                BookId = BookId
            };

            await _context.AuthorBooks.AddAsync(AuthorBook);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveByAuthorId(int Id)
        {
            var authorBook = await _context.AuthorBooks.Where(n => n.AuthorId == Id).ToListAsync();
            _context.AuthorBooks.RemoveRange(authorBook);

            await _context.SaveChangesAsync();
        }

        public async Task RemoveByBookId(int Id)
        {
            var authorBook = await _context.AuthorBooks.Where(n => n.BookId == Id).ToListAsync();
            _context.AuthorBooks.RemoveRange(authorBook);

            await _context.SaveChangesAsync();
        }
    }
}
