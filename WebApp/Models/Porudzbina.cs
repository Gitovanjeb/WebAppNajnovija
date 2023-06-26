using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public class Porudzbina
    {
        int id;
        int proizvodId;
        int kolicina;
        string kupac;
        DateTime datumPorudzbine;
        string status;

        public Porudzbina() {}
        public Porudzbina(int proizvodID, int kolicina, string kupac, DateTime datumPorudzbine, string status)
        {
            this.proizvodId = proizvodID;
            this.kolicina = kolicina;
            this.kupac = kupac;
            this.datumPorudzbine = datumPorudzbine;
            this.status = status;
        }

        public int ProizvodID { get => proizvodId; set => proizvodId = value; }
        public int Kolicina { get => kolicina; set => kolicina = value; }
        public string Kupac { get => kupac; set => kupac = value; }
        public DateTime DatumPorudzbine { get => datumPorudzbine; set => datumPorudzbine = value; }
        public string Status { get => status; set => status = value; }
        public int Id { get => id; set => id = value; }

        public override string ToString()
        {
            return $"{ProizvodID},{Kolicina},{Kupac},{DatumPorudzbine},{Status}";
        }

        public static Porudzbina FromString(string input)
        {
            string[] vrednosti = input.Split(',');
            Porudzbina porudzbina = new Porudzbina
            {
                proizvodId = int.Parse(vrednosti[0]),
                kolicina = int.Parse(vrednosti[1]),
                kupac = vrednosti[2],
                datumPorudzbine = DateTime.Parse(vrednosti[3]),
                status = vrednosti[4]
            };
            return porudzbina;
        }
    }
}