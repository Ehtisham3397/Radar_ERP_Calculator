
using Microsoft.EntityFrameworkCore;

namespace Calculator_Radar_Params
{
    public class DataContext : DbContext
    {
        private static bool _created = false;
        public DataContext()
        {
            if (!_created)
            {
                _created = true;
                Database.EnsureCreated();  // Ensures database is created
            }
        }
        public DbSet<Data> RadarData { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source = RadarData.db");
        }
    }
}
