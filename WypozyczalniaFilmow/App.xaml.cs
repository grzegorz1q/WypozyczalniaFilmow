using Microsoft.EntityFrameworkCore;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Windows;
using WypozyczalniaFilmow.Database;

namespace WypozyczalniaFilmow
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            using (var context = new DesignTimeDbContextFactory().CreateDbContext(null))
            {
                context.Database.Migrate();
                var seeder = new DatabaseSeeder(context);
                seeder.Seed(); // Dodajemy dane do bazy
            }
        }
    }

}
