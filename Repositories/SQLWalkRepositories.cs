using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
    public class SQLWalkRepositories : IWalkRepository
    {
        private readonly NZWalksDbContext dbContext;

        public SQLWalkRepositories(NZWalksDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<Walk> CreateAsync(Walk walk)
        {
            //throw new NotImplementedException();
            await dbContext.Walks.AddAsync(walk);
            await dbContext.SaveChangesAsync();
            return walk;
        }

        public async Task<List<Walk>> GetAllAsync()
        {
            //throw new NotImplementedException();
            return await dbContext.Walks.Include("Difficulty").Include("Region").ToListAsync();

        }
    }
}
