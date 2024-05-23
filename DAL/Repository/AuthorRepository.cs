using DAL.Filters;
using Entities;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repository
{
    public class AuthorRepository : IRepository<Author>
    {
        private readonly DataContext _context;

        public AuthorRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<Author?> Get(int id)
        {
            return await _context.Authors.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<Author>> Get(IFilter filter)
        {
            var authorFilter = (AuthorFilter)filter;

            var query = _context.Authors.AsQueryable();

            if (authorFilter.ById.HasValue)
                query = query.Where(x => x.Id == authorFilter.ById);

            if (!string.IsNullOrWhiteSpace(authorFilter.AuthorName))
                query = query.Where(x => x.Firstname.Contains(authorFilter.AuthorName, StringComparison.OrdinalIgnoreCase) || 
                                         x.Lastname.Contains(authorFilter.AuthorName, StringComparison.OrdinalIgnoreCase));

            if (authorFilter.BornAfter.HasValue)
                query = query.Where(x => x.Born >= authorFilter.BornAfter);

            if (authorFilter.BornBefore.HasValue)
                query = query.Where(x => x.Born <= authorFilter.BornBefore);

            return await query.ToListAsync();
        }

        public async Task<Author?> Post(Author entity)
        {
            _context.Authors.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<Author?> Put(Author entity)
        {
            var result = await _context.Authors.FirstOrDefaultAsync(x => x.Id == entity.Id);
            if (result == null)
                return null;

            _context.Attach(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<Author?> Delete(int id)
        {
            var result = await _context.Authors.FirstOrDefaultAsync(x => x.Id == id);
            if (result == null)
                return null;

            _context.Authors.Remove(result);
            await _context.SaveChangesAsync();
            return result;
        }
    }
}
