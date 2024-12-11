using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WypozyczalniaFilmow.Models
{
    public class Film
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string? Director { get; set; }
        public string? Category { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string? Description { get; set; }
        public string? Cover { get; set; }
        public int Count { get; set; }
        public virtual ICollection<Rent> Rents { get; set; }
       // public virtual ICollection<ActorFilm> ActorFilms { get; set; }
        public virtual ICollection<Actor> Actors { get; set; }
    }
}
