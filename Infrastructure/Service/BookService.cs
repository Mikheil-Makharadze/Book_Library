using Core.Entities;
using Core.Interface;
using Infrastructure.CustomeException;
using Infrastructure.Data.AppDB;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Service
{
    public class BookService : GenericRepository<Book>, IBookService
    {
        private readonly AppDbContext _context;

        public BookService(AppDbContext context) : base(context)
        {
            _context = context;
        }
        //public override Task<IEnumerable<Book>> GetAllAsync()
        //{
        //    return base.GetAllAsync();
        //}

        public async Task<IEnumerable<Book>> GetAllDetailsAsync(string? search)
        {
            var query = _context.Books.AsNoTracking().AsQueryable();

            if(search != null)
            {
                query = query.Where(n => n.Title != null && n.Title.Contains(search.Trim()));
            }

            return await query.Include(n => n.AuthorBooks).ThenInclude(n => n.Author).ToListAsync();
        }

        public async Task<Book> GetByIdDetailsAsync(int id)
        {
            return await _context.Books.AsNoTracking().AsQueryable()
                .Include(n => n.AuthorBooks).ThenInclude(n => n.Author)
                .FirstOrDefaultAsync(n => n.Id == id) ?? throw new NotFoundException($"Book with Id: {id} was not found");
        }
    }
}