using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApp.Models.Bindings
{
    public class ProizvodBindingModel
    {
        [Required]
        [MinLength(4)]
        [MaxLength(50)]
        public string Ime { get; set; }

        [Required]
        public double Cena { get; set; }

        [Required]
        public int Kolicina { get; set; }

        [Required]
        [MinLength(4)]
        [MaxLength(50)]
        public string Opis { get; set; }

        [Required]
        public string Slika { get; set; }

        [Required]
        public string Grad { get; set; }
    }
}