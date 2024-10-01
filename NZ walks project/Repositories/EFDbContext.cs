using Microsoft.EntityFrameworkCore;
using NZWalks.API.Entity_Models.Domain;

namespace NZWalks.API.Repositories
{
    public class EFDbContext: DbContext
    {
        public EFDbContext(DbContextOptions dbContextOptions): base(dbContextOptions) 
        {
                
        }

        public DbSet <Difficulty> Difficulties { get; set; }
        public DbSet <Region> Regions { get; set; }
        public DbSet <Walk> Walks { get; set; }

    }
}
