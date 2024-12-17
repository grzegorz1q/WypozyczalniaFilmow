using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using WypozyczalniaFilmow.Models;

namespace WypozyczalniaFilmow.Database
{
    public class AppDbContext : DbContext
    {
        public virtual DbSet<Actor> Actors { get; set; }
        public virtual DbSet<Film> Films { get; set; }
        public virtual DbSet<Person> Persons { get; set; }
        public virtual DbSet<Rent> Rents { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Person>()
                .ToTable("Persons")
                .HasDiscriminator<int>("Role")
                .HasValue<Person>((int)Role.Person)
                .HasValue<Client>((int)Role.Client)
                .HasValue<Actor>((int)Role.Actor);


            modelBuilder.Entity<Actor>()
                .HasMany(f => f.Films)
                .WithMany(a => a.Actors);

            modelBuilder.Entity<Rent>()
                .HasKey(d => new { d.ClientId, d.FilmId });

            modelBuilder.Entity<Rent>()
                .HasOne(c => c.Client)
                .WithMany(r => r.Rents)
                .HasForeignKey(c => c.ClientId);

            modelBuilder.Entity<Rent>()
                .HasOne(f => f.Film)
                .WithMany(r => r.Rents)
                .HasForeignKey(f => f.FilmId)
                .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
