using Microsoft.EntityFrameworkCore;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Data
{
    public class NZWalksDbContext: DbContext
    {
        public NZWalksDbContext(DbContextOptions dbContextOptions): base(dbContextOptions)
        {
            
        }

        public DbSet<Difficulty> Difficulties { get; set; }
        public DbSet<Region> Regions { get; set; }

        public DbSet<Walk> Walks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //seed the data for dificulty
            var difficulties = new List<Difficulty>()
            {
                new Difficulty()
                {
                    Id = Guid.Parse("cc100231-85b8-4605-935e-e890a69bfe6f"),
                    Name = "Easy"
                },
                new Difficulty()
                {
                    Id = Guid.Parse("8c97bc97-0a27-4bc4-8223-e16e692ec094"),
                    Name = "Medium"
                },
                new Difficulty()
                {
                    Id = Guid.Parse("cf74a845-d4ba-4bc3-9f91-23951863266c"),
                    Name = "Hard"
                }
            };
            //Seeding the data into the db

            modelBuilder.Entity<Difficulty>().HasData(difficulties);

            //seed data for regions
            var regions = new List<Region>()
            {
                new Region()
                {
                    Id = Guid.Parse("5cd4f596-e9f6-43e3-a2c8-56ce00636ce2"),
                    Name = "Nairobi",
                    Code = "NBO",
                    RegionImageUrl = "nairobi/image"
                    
                },
                new Region
                {
                    Id = Guid.Parse("6884f7d7-ad1f-4101-8df3-7a6fa7387d81"),
                    Name = "Northland",
                    Code = "NTL",
                    RegionImageUrl = null
                },
                new Region
                {
                    Id = Guid.Parse("14ceba71-4b51-4777-9b17-46602cf66153"),
                    Name = "Bay Of Plenty",
                    Code = "BOP",
                    RegionImageUrl = null
                },
                new Region
                {
                    Id = Guid.Parse("cfa06ed2-bf65-4b65-93ed-c9d286ddb0de"),
                    Name = "Wellington",
                    Code = "WGN",
                    RegionImageUrl = "https://images.pexels.com/photos/4350631/pexels-photo-4350631.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1"
                },
                new Region
                {
                    Id = Guid.Parse("906cb139-415a-4bbb-a174-1a1faf9fb1f6"),
                    Name = "Nelson",
                    Code = "NSN",
                    RegionImageUrl = "https://images.pexels.com/photos/13918194/pexels-photo-13918194.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1"
                },
                new Region
                {
                    Id = Guid.Parse("f077a22e-4248-4bf6-b564-c7cf4e250263"),
                    Name = "Southland",
                    Code = "STL",
                    RegionImageUrl = null
                }
            };

            //seed the data
            modelBuilder.Entity<Region>().HasData(regions);

        }
    }
}
