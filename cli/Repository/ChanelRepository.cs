using Microsoft.EntityFrameworkCore;
using RSSRepository;
using RSSRepository.Context;
using RSSRepository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace cli.Repository
{
    public class ChanelRepository : IRepository<RSSChanel>
    {
        private readonly RSSContext _dbContext;

        public ChanelRepository(RSSContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> Create(RSSChanel entity)
        {
            await _dbContext.Set<RSSChanel>().AddAsync(entity);
            await _dbContext.SaveChangesAsync();

            return true;
        }

        public async Task Delete(string id)
        {
            var entity = await GetById(id);
            _dbContext.Set<RSSChanel>().Remove(entity);
            await _dbContext.SaveChangesAsync();
        }

        public IQueryable<RSSChanel> GetAll()
        {
            return _dbContext.Set<RSSChanel>().AsNoTracking();
        }

        public async Task<RSSChanel> GetById(string id)
        {
            return await _dbContext.Set<RSSChanel>()
                 .AsNoTracking()
                 .FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task Update(string id, RSSChanel entity)
        {
            _dbContext.Set<RSSChanel>().Update(entity);
            await _dbContext.SaveChangesAsync();
        }
    }
}
