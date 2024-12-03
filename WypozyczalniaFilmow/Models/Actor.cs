using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WypozyczalniaFilmow.Models
{
    public class Actor : Person
    {
        //public virtual ICollection<ActorFilm> ActorFilms { get; set; }
        public virtual ICollection<Film> Films { get; set; }
    }
}
