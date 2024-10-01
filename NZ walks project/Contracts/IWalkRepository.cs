using NZWalks.API.Entity_Models.Domain;

namespace NZWalks.API.Contracts
{
    public interface IWalkRepository
    {
        Task<Walk> CreateAsync(Walk walk);
        Task<List<Walk>> GetAllAsync();
        Task<Walk> GetByIdAsync(int id);
        Task<Walk> UpdateWalkAsync(int id, Walk walk);
        Task<Walk> DeleteWalkAsync(int id);
        Task SaveChangesAsync();
    }
}
