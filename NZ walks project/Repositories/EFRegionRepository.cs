using NZWalks.API.Entity_Models.Domain;
using NZWalks.API.Contracts;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace NZWalks.API.Repositories
{
    public class EFRegionRepository : IRegionRepository
    {
        private readonly EFDbContext _dbContext;

        public EFRegionRepository(EFDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Region>> GetAllRegionAsync()
        {
            return await _dbContext.Regions.ToListAsync();
        }

        public async Task<Region> GetRegionByIdAsync(int id)
        {
            return await _dbContext.Regions.FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<Region> AddRegionAsync(Region region)
        {
            await _dbContext.Regions.AddAsync(region);
            await _dbContext.SaveChangesAsync();
            return region;
        }

        public async Task<Region?> UpdateRegionAsync(int id)
        {
            return  await _dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);

        }

        public async Task<Region?> DeleteRegionAsync(int id)
        {
            return await _dbContext.Regions.FirstOrDefaultAsync(r => r.Id == id);


         }

        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
