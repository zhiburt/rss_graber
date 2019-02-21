using Microsoft.EntityFrameworkCore;
using RSSRepository.Context;
using RSSRepository.Models;
using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace cli.Repository
{
    public class RSSRepository : IRepository<RSS>
    {
        private readonly RSSContext _dbContext;

        public RSSRepository(RSSContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> Create(RSS entity)
        {
            entity.Id = Guid.NewGuid().ToString();

            var exist = await ExistRSSAsync(entity);
            if (exist == null)
            {
                await _dbContext.Set<RSS>().AddAsync(entity);
                await _dbContext.SaveChangesAsync();
                return true;
            }

            return false;
        }

        private async Task<RSS> ExistRSSAsync(RSS entity)
        {
            var all = GetAll();
            if (all == null)
            {
                return null;
            }

            return await all.FirstOrDefaultAsync(rss => rss.Title == entity.Title &&
                                                        rss.Date == entity.Date);
        }

        public async Task Delete(string id)
        {
            var entity = await GetById(id);
            _dbContext.Set<RSS>().Remove(entity);
            await _dbContext.SaveChangesAsync();
        }

        public IQueryable<RSS> GetAll()
        {
            return _dbContext.Set<RSS>().AsNoTracking();
        }

        public async Task<RSS> GetById(string id)
        {
            return await _dbContext.Set<RSS>()
                 .AsNoTracking()
                 .FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task Update(string id, RSS entity)
        {
            _dbContext.Set<RSS>().Update(entity);
            await _dbContext.SaveChangesAsync();
        }
    }
}
