using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApp.Models.Bindings
{
    public class UpdateProfilModel
    {
        [MinLength(4)]
        [MaxLength(50)]
        public string Ime { get; set; }


        [MinLength(4)]
        [MaxLength(50)]
        public string Prezime { get; set; }

        [MinLength(4)]
        [MaxLength(50)]
        public string Lozinka { get; set; }

        [MinLength(4)]
        [MaxLength(50)]
        public string Email { get; set; }

        [MinLength(4)]
        [MaxLength(50)]
        public string DatumRodjenja { get; set; }

        [MinLength(4)]
        [MaxLength(50)]
        public string KorisnickoIme { get; set; }
    }
}