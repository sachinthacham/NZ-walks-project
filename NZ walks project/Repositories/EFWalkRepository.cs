using NZWalks.API.Contracts;
using NZWalks.API.Repositories;
using NZWalks.API.Entity_Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace NZWalks.API.EFRepositories
{
    public class EFWalkRepository: IWalkRepository
    {
        private readonly EFDbContext _dbContext;

        public EFWalkRepository(EFDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Walk> CreateAsync(Walk walk)
        {
            await _dbContext.Walks.AddAsync(walk);
            await _dbContext.SaveChangesAsync();
            return walk;
        }

        public async Task<List<Walk>> GetAllAsync()
        {
            return await _dbContext.Walks.Include("Difficulty").Include("Region").ToListAsync();
        }

        public async Task<Walk> GetByIdAsync(int id)
        {
            return await _dbContext.Walks.Include("Difficulty").Include("Region").FirstOrDefaultAsync(x =>  x.Id == id);
        }

        public async Task<Walk> UpdateWalkAsync(int id, Walk walk)
        {
            var existingWalk = await _dbContext.Walks.FirstOrDefaultAsync(x => x.Id == id);

            if (existingWalk == null)
            {
                return null;
            }

            existingWalk.Name = walk.Name;
            existingWalk.Description = walk.Description;
            existingWalk.LengthInKm = walk.LengthInKm;
            existingWalk.WalkImageUrl = walk.WalkImageUrl;
            existingWalk.DifficultyId = walk.DifficultyId;
            existingWalk.RegionId = walk.RegionId;

            await _dbContext.SaveChangesAsync();
            return existingWalk;
                 
    }

        public async Task<Walk> DeleteWalkAsync(int id)
        {
            var deletedWalk = await _dbContext.Walks.FirstOrDefaultAsync(x => x.Id ==id);   
            if(deletedWalk == null)
            {
                return null;
            }

            _dbContext.Walks.Remove(deletedWalk);
            await _dbContext.SaveChangesAsync();
            return deletedWalk;
        }

        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
