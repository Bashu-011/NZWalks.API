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

        public async Task<Walk?> DeleteAsync(Guid id)
        {
            //throw new NotImplementedException();
            var existingWalk = await dbContext.Walks.FirstOrDefaultAsync(x => x.Id == id);
            if (existingWalk == null)
            {
                return null;
            }
            else
            {
                dbContext.Walks.Remove(existingWalk);
                await dbContext.SaveChangesAsync();
                return existingWalk;
            }
        }

        public async Task<List<Walk>> GetAllAsync(string? filterOn = null, string? filterQuery = null,
            string? sortBy = null, bool? isAscending = true)
        {
            //implementing filtering
            var walks = dbContext.Walks.Include("Difficulty").Include("Region").AsQueryable();

            //filtering
            if (string.IsNullOrWhiteSpace(filterOn) == false && string.IsNullOrWhiteSpace(filterQuery) == false)
            {
                if (filterOn.Equals("Name", StringComparison.OrdinalIgnoreCase)) {
                    walks = walks.Where(x => x.Name.Contains(filterQuery));
            } 


            } 
            //sorting
            if (string.IsNullOrWhiteSpace(sortBy) == false)
            {
                if(sortBy.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    //walks = isAscending ? walks.OrderBy(x => x.Name): walks.OrderByDescending(x => x.Name);
                    walks = (isAscending ?? true) ? walks.OrderBy(x => x.Name) : walks.OrderByDescending(x => x.Name);

                }
                else if (sortBy.Equals("Length", StringComparison.OrdinalIgnoreCase))
                {
                    //walks = isAscending ? walks.OrderBy(x => x.LengthInKm): walks.OrderByDescending(x => x.LengthInKm);
                    walks = (isAscending ?? true) ? walks.OrderBy(x => x.LengthInKm) : walks.OrderByDescending(x => x.LengthInKm);

                }
            }

            return await walks.ToListAsync();

            //throw new NotImplementedException();
           // return await dbContext.Walks.Include("Difficulty").Include("Region").ToListAsync();
           
        }

        public async Task<Walk?> GetWalkByIdAsync(Guid id)
        {
            //throw new NotImplementedException();
            var walk = await dbContext.Walks.Include("Difficulty").Include("Region").FirstOrDefaultAsync(x => x.Id == id);
            return walk;
        }

        public async Task<Walk?> UpdateAsync(Guid id, Walk walk)
        {
            //throw new NotImplementedException();
            var existingWalk = await dbContext.Walks.FirstOrDefaultAsync(x => x.Id == id);

            if (existingWalk == null)
            {
                return null;
            }
            else
            {
                existingWalk.Name = walk.Name;
                existingWalk.Description = walk.Description;
                existingWalk.RegionId = walk.RegionId;
                existingWalk.LengthInKm = walk.LengthInKm;
                existingWalk.DifficultyID = walk.DifficultyID;
                existingWalk.WalkImageUrl = walk.WalkImageUrl;

                await dbContext.SaveChangesAsync();
                return existingWalk;
            }
        }
    }
}
