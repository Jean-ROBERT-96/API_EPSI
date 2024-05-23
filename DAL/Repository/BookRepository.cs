using DAL.Filters;
using Entities;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repository
{
    public class BookRepository : IRepository<Book>
    {
        private readonly DataContext _context;

        public BookRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<Book?> Get(int id)
        {
            return await _context.Books.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<Book>> Get(IFilter filter)
        {
            var bookFilter = (BookFilter)filter;
            var query = _context.Books.AsQueryable();

            if (bookFilter.ById.HasValue)
                query = query.Where(x => x.Id == bookFilter.ById);

            if (!string.IsNullOrWhiteSpace(bookFilter.TextSearch))
                query = query.Where(x => x.Title.Contains(bookFilter.TextSearch, StringComparison.OrdinalIgnoreCase) ||
                                         x.Description.Contains(bookFilter.TextSearch, StringComparison.OrdinalIgnoreCase));

            if (!string.IsNullOrWhiteSpace(bookFilter.AuthorName))
                query = query.Where(x => x.Author.Firstname.Contains(bookFilter.AuthorName, StringComparison.OrdinalIgnoreCase) ||
                                         x.Author.Lastname.Contains(bookFilter.AuthorName, StringComparison.OrdinalIgnoreCase));

            if (!string.IsNullOrWhiteSpace(bookFilter.PublisherName))
                query = query.Where(x => x.Publisher.Name.Contains(bookFilter.PublisherName, StringComparison.OrdinalIgnoreCase));

            if (bookFilter.ReleaseAfter.HasValue)
                query = query.Where(x => x.Release >= bookFilter.ReleaseAfter);

            if (bookFilter.ReleaseBefore.HasValue)
                query = query.Where(x => x.Release <= bookFilter.ReleaseBefore);

            return await query.ToListAsync();
        }

        public async Task<Book?> Post(Book entity)
        {
            _context.Books.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<Book?> Put(Book entity)
        {
            var result = await _context.Books.FirstOrDefaultAsync(x => x.Id == entity.Id);
            if (result == null)
                return null;

            _context.Attach(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<Book?> Delete(int id)
        {
            var result = await _context.Books.FirstOrDefaultAsync(x => x.Id == id);
            if (result == null)
                return null;

            _context.Books.Remove(result);
            await _context.SaveChangesAsync();
            return result;
        }
    }
}
