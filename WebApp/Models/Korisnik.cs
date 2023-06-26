using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public abstract class Korisnik
    {
        string korisnickoIme;
        string lozinka;
        string ime;
        string prezime;
        Pol pol;
        string email;
        DateTime datumRodjenja;
        Uloga uloga;
        bool isDeleted;

        public string KorisnickoIme { get => korisnickoIme; set => korisnickoIme = value; }
        public string Lozinka { get => lozinka; set => lozinka = value; }
        public string Ime { get => ime; set => ime = value; }
        public string Prezime { get => prezime; set => prezime = value; }
        public Pol Pol { get => pol; set => pol = value; }
        public string Email { get => email; set => email = value; }
        public DateTime DatumRodjenja { get => datumRodjenja; set => datumRodjenja = value; }
        public Uloga Uloga { get => uloga; set => uloga = value; }
        public bool IsDeleted { get => isDeleted; set => isDeleted = value; }

        public override string ToString()
        {
            return $"{KorisnickoIme},{Lozinka},{Ime},{Prezime},{Pol},{Email},{DatumRodjenja},{Uloga},{isDeleted}";
        }

        public static Korisnik Parse(string korisnikString)
        {
            Korisnik korisnik = new Kupac();

            string[] properties = korisnikString.Split(',');

            //if (properties.Length != 8)
            //{
            //    throw new ArgumentException("Invalid format for korisnikString. Expected 8 properties separated by commas.");
            //}

            korisnik.KorisnickoIme = properties[0];
            korisnik.Lozinka = properties[1];
            korisnik.Ime = properties[2];
            korisnik.Prezime = properties[3];
            korisnik.Pol = (Pol)Enum.Parse(typeof(Pol), properties[4]);
            korisnik.Email = properties[5];
            korisnik.DatumRodjenja = DateTime.Parse(properties[6]);
            korisnik.Uloga = (Uloga)Enum.Parse(typeof(Uloga), properties[7]);
           // korisnik.isDeleted = bool.Parse(properties[8]);

            return korisnik;
        }

    }
}