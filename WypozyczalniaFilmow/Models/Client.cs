using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WypozyczalniaFilmow.Models
{
    public class Client : Person
    {
        public string Email { get; set; }
        public int PhoneNumber { get; set; }
        public virtual ICollection<Rent> Rents { get; set; }
    }
}
