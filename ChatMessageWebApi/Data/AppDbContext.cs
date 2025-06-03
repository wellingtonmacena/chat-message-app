using ChatMessageWebApi.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace ChatMessageWebApi.Data
{
    public class AppDbContext(DbContextOptions<AppDbContext> dbContextOptions) : DbContext(dbContextOptions)
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Message> Messages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            base.OnModelCreating(modelBuilder);


            List<User> users = new()
                {
                    new()
                    {
                        CreatedAt = DateTime.Parse("10/05/2025 00:26:40"),
                        Email = "jole.boone@googlemail.com",
                        Id = Guid.Parse("7a13aefc-083f-4823-8bff-974fe0e8255d"),
                        ProfilePictureUrl = "https://i.pravatar.cc/40?img=6",
                        Name = "Jole Boone",
                        Password = "poster"
                    },
                    new()
                    {
                        CreatedAt = DateTime.Parse("10/05/2025 00:26:40"),
                        Email = "nevin.swanson@outlook.com",
                        ProfilePictureUrl = "https://i.pravatar.cc/40?img=8",
                        Id = Guid.Parse("1791b331-ab29-46b4-9c88-e3555ae96bfd"),
                        Name = "Nevin Swanson",
                        Password = "poster"
                    },
                      new()
                    {
                        CreatedAt = DateTime.Parse("10/05/2025 00:26:40"),
                        Email = "aydin.welch@rediffmail.com",
                        ProfilePictureUrl = "https://i.pravatar.cc/40?img=4",
                         Id = Guid.Parse("d07da0e7-195a-4858-8c72-eaa77a5d9601"),
                        Name = "Aydin Welch",
                        Password = "poster"
                    },
                    new()
                    {
                        CreatedAt = DateTime.Parse("10/05/2025 00:26:40"),
                        Email = "erika.bolton@rediffmail.com",
                        ProfilePictureUrl = "https://i.pravatar.cc/40?img=1",
                         Id = Guid.Parse("32098499-3a9e-4337-a16a-8199f53510d9"),
                        Name = "Erika Bolton",
                        Password = "poster"
                    },
                    new()
                    {
                        CreatedAt = DateTime.Parse("10/05/2025 00:26:40"),
                        Email = "well.Sanss@rediffmail.com",
                         Id = Guid.Parse("e828780a-778a-4c70-92f5-f3f0f40d743c"),
                        ProfilePictureUrl = "https://i.pravatar.cc/40?img=3",
                        Name = "Well Sanss",
                        Password = "poster"
                    }
                };

            modelBuilder.Entity<User>().HasData(users);



        }

    }
}
