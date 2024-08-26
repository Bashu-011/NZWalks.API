using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
    public class SQLRegionRepository : IRegionRepository
    {
        private readonly NZWalksDbContext dbContext;

        public SQLRegionRepository(NZWalksDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Region> CreateAsync(Region region)
        {
            //throw new NotImplementedException();
            await dbContext.Regions.AddAsync(region);
            await dbContext.SaveChangesAsync();
            return region;
        }

        public async Task<Region?> DeleteAsync(Guid id)
        {
            //throw new NotImplementedException();
            var existingRegion = await dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
            if (existingRegion == null)
            {
                return null;
            }
            else
            {
                dbContext.Regions.Remove(existingRegion);
                await dbContext.SaveChangesAsync();
                return existingRegion;
            }
        }

        public async Task<List<Region>> GetAllAsync()
        {
            //throw new NotImplementedException();
            var regionsModel = await dbContext.Regions.ToListAsync();
            return regionsModel;
        }

        public async Task<Region?> GetByIdAsync(Guid id)
        {
            //throw new NotImplementedException();
            var region = await dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
            return region;
        }

        public async Task<Region?> UpdateAsync(Guid id, Region region)
        {
            //throw new NotImplementedException();
            var existingRegion = await dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);

            if (existingRegion == null)
            {
                return null;
            }
            else
            {
                existingRegion.Code = region.Code;
                existingRegion.Name = region.Name;
                existingRegion.RegionImageUrl = region.RegionImageUrl;

                await dbContext.SaveChangesAsync();
                return existingRegion;
            }

        }
    }
}
