using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace WypozyczalniaFilmow.Database
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            //optionsBuilder.UseSqlite("Data Source=WypozyczalniaFilmow.db");
            optionsBuilder.UseInMemoryDatabase("InMemoryDb");
            return new AppDbContext(optionsBuilder.Options);
        }
    }
}