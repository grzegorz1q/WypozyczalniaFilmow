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
                var seeder = new DatabaseSeeder(context);
                seeder.Seed(); // Dodajemy dane do bazy
                var films = context.Films.ToList();
                foreach (var film in films)
                {
                    if (film.Title == "Skazani na Shawshank")
                    {
                        foreach (var actor in film.Actors)
                        {
                            Debug.WriteLine(actor.Name);
                        }
                    }
                }
            }
        }
    }

}
