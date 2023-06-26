using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public class Kupac : Korisnik
    {
        List<int> porudzbine = new List<int>();
        List<int> omiljeniProizvodi = new List<int>();

        public List<int> Porudzbine { get => porudzbine; set => porudzbine = value; }
        public List<int> OmiljeniProizvodi { get => omiljeniProizvodi; set => omiljeniProizvodi = value; }

        public override string ToString()
        {
            string porudzbineString = string.Join(";", Porudzbine);
            string omiljeniProizvodiString = string.Join(";", OmiljeniProizvodi);

            return $"{KorisnickoIme},{Lozinka},{Ime},{Prezime},{Pol},{Email},{DatumRodjenja},{Uloga},|{porudzbineString}|,|{omiljeniProizvodiString}|,{IsDeleted}";
        }

        public static Kupac Parse(string korisnikString)
        {
            Kupac kupac = new Kupac();

            string[] vrednosti = korisnikString.Split(',');

            if (vrednosti.Length != 11)
            {
                throw new ArgumentException("Invalid format for korisnikString. Expected 9 vrednosti separated by commas.");
            }

            kupac.KorisnickoIme = vrednosti[0];
            kupac.Lozinka = vrednosti[1];
            kupac.Ime = vrednosti[2];
            kupac.Prezime = vrednosti[3];
            kupac.Pol = (Pol)Enum.Parse(typeof(Pol), vrednosti[4]);
            kupac.Email = vrednosti[5];
            kupac.DatumRodjenja = DateTime.Parse(vrednosti[6]);
            kupac.Uloga = (Uloga)Enum.Parse(typeof(Uloga), vrednosti[7]);

            string porudzbineString = vrednosti[8].TrimStart('|').TrimEnd('|');
            
            kupac.Porudzbine = new List<int>();

            if(!porudzbineString.Equals(""))
            {
                string[] porudzbineArray = porudzbineString.Split(';');
                foreach (string porudzbina in porudzbineArray)
                {
                    kupac.Porudzbine.Add(int.Parse(porudzbina));
                }
            }

            string omiljeniProizvodiString = vrednosti[9].TrimStart('|').TrimEnd('|');
            kupac.OmiljeniProizvodi = new List<int>();
            if (!omiljeniProizvodiString.Equals(""))
            {
                string[] omiljeniProizvodiArray = omiljeniProizvodiString.Split(';');
                foreach (string porudzbina in omiljeniProizvodiArray)
                {
                    kupac.Porudzbine.Add(int.Parse(porudzbina));
                }
            }
            kupac.IsDeleted = bool.Parse(vrednosti[10]);

            return kupac;
        }
    }
}