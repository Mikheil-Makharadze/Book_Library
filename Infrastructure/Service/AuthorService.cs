using Core.Entities;
using Core.Interface;
using Infrastructure.Data.AppDB;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Service
{
    public class AuthorService : GenericRepository<Author>, IAuthorService
    {
        private readonly AppDbContext _context;

        public AuthorService(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Author>> GetAllDetailsAsync(string? search)
        {
            var query = _context.Authors.AsNoTracking().AsQueryable();

            if (search != null)
            {
                query = query.Where(n => n.Name.Contains(search.Trim()));
            }

            return await query.Include(n => n.AuthorBooks).ThenInclude(n => n.Book).ToListAsync();
        }

        public async Task<Author> GetByIdDetailsAsync(int id)
        {
            return await _context.Authors.AsNoTracking().AsQueryable()
                .Include(n => n.AuthorBooks).ThenInclude(n => n.Book).FirstOrDefaultAsync(n => n.Id == id);
        }
    }
}