using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public class Prodavac : Korisnik
    {
        List<int> objavljeniProizvodi = new List<int>();

        public List<int> ObjavljeniProizvodi { get => objavljeniProizvodi; set => objavljeniProizvodi = value; }

        public override string ToString()
        {
            string objavljeniProizvodiString = string.Join(",", ObjavljeniProizvodi);

            return $"{KorisnickoIme},{Lozinka},{Ime},{Prezime},{Pol},{Email},{DatumRodjenja},{Uloga},|{objavljeniProizvodiString}|";
        }

        public static Prodavac Parse(string prodavacString)
        {
            Prodavac prodavac = new Prodavac();
            string[] vrednosti = prodavacString.Split(',');

            prodavac.KorisnickoIme = vrednosti[0];
            prodavac.Lozinka = vrednosti[1];
            prodavac.Ime = vrednosti[2];
            prodavac.Prezime = vrednosti[3];
            prodavac.Pol = (Pol)Enum.Parse(typeof(Pol), vrednosti[4]);
            prodavac.Email = vrednosti[5];
            prodavac.DatumRodjenja = DateTime.Parse(vrednosti[6]);
            prodavac.Uloga = (Uloga)Enum.Parse(typeof(Uloga), vrednosti[7]);

            string objavljeniProizvodiString = vrednosti[8].TrimStart('|').TrimEnd('|');

            prodavac.ObjavljeniProizvodi = new List<int>();

            if (!objavljeniProizvodiString.Equals(""))
            {
                string[] objavljeniProizvodiArray = objavljeniProizvodiString.Split(',');
                foreach (string proizvod in objavljeniProizvodiArray)
                {
                    prodavac.ObjavljeniProizvodi.Add(int.Parse(proizvod));
                }
            }

            return prodavac;
        }
    }
}