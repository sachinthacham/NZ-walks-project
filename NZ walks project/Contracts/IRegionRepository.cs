using NZWalks.API.Entity_Models.Domain;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace NZWalks.API.Contracts
{
    public interface IRegionRepository
    {
        Task<List<Region>> GetAllRegionAsync();
        Task<Region?> GetRegionByIdAsync(int id); 
        Task<Region> AddRegionAsync(Region region); 
        Task<Region?> UpdateRegionAsync(int id); 
        Task<Region?> DeleteRegionAsync(int id); 
        Task SaveChangesAsync();
        
    }
}
