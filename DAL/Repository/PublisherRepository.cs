using DAL.Filters;
using Entities;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repository
{
    public class PublisherRepository : IRepository<Publisher>
    {
        private readonly DataContext _context;

        public PublisherRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<Publisher?> Get(int id)
        {
            return await _context.Publishers.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<Publisher>> Get(IFilter filter)
        {
            var publisherFilter = (PublisherFilter)filter;

            var query = _context.Publishers.AsQueryable();

            if(publisherFilter.ById.HasValue)
                query = query.Where(x => x.Id == publisherFilter.ById);

            if(!string.IsNullOrWhiteSpace(publisherFilter.Name))
                query = query.Where(x => x.Name.Contains(publisherFilter.Name));

            if(publisherFilter.FoundedAfter.HasValue)
                query = query.Where(x => x.Founding >= publisherFilter.FoundedAfter);

            if (publisherFilter.FoundedBefore.HasValue)
                query = query.Where(x => x.Founding <= publisherFilter.FoundedBefore);

            return await query.ToListAsync();
        }

        public async Task<Publisher?> Post(Publisher entity)
        {
            _context.Publishers.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<Publisher?> Put(Publisher entity)
        {
            var result = await _context.Publishers.FirstOrDefaultAsync(x => x.Id == entity.Id);
            if (result == null)
                return null;

            _context.Attach(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<Publisher?> Delete(int id)
        {
            var result = await _context.Publishers.FirstOrDefaultAsync(x => x.Id == id);
            if (result == null)
                return null;

            _context.Publishers.Remove(result);
            await _context.SaveChangesAsync();
            return result;
        }
    }
}
