using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace EntityFrameworkBasics.Data
{
    //The database representational model for our application
    public class ApplicationDbContext : DbContext
    {
        #region Public Properties
        public DbSet<SettingsDataModel> Settings { get; set; }
        #endregion

        public ApplicationDbContext(DbContextOptions options) // PASSING IN DBCONTEXTOPTIONS AS AN ARGUMENT FOR DB CREATION SO YOU CAN USE DI IN THE STARTUP
                : base(options)
        {
        }

        public ApplicationDbContext()
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)//configure db
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseNpgsql("Server=localhost;Port=5432;Database=Settings;");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)//where you define relationships between tables public key etc
        {
            base.OnModelCreating(modelBuilder);
        }
    }

}
