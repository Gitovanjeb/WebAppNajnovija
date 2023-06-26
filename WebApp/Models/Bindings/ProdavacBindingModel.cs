using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApp.Models.Bindings
{
    public class ProdavacBindingModel
    {
        [Required]
        [MinLength(4)]
        [MaxLength(50)]
        public string KorisnickoIme { get; set; }

        [Required]
        [MinLength(4)]
        [MaxLength(50)]
        public string Ime { get; set; }

        [Required]
        [MinLength(4)]
        [MaxLength(50)]
        public string Prezime { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Lozinka { get; set; }

        [Required]
        public string Pol { get; set; }

        [Required]
        public string DatumRodjenja { get; set; }
    }
}