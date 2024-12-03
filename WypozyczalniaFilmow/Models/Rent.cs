using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WypozyczalniaFilmow.Models
{
    public class Rent
    {
        [Key]
        public int Id { get; set; }
        public int ClientId { get; set; }
        public virtual Client Client { get; set; }
        public int FilmId { get; set; }
        public virtual Film Film { get; set; }
        public DateTime RentDate { get; set; } = DateTime.Now;
    }
}
