using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public class Proizvod
    {
        int id;
        string ime;
        double cena;
        int kolicina;
        string opis;
        string slika;
        DateTime datum;
        string grad;
        List<int> listaRecenzija;
        bool statusDostupnosti;
        bool isDeleted;

        private static int counter = 1;

        public Proizvod()
        {
            Id = counter++;
            IsDeleted = false;
            listaRecenzija = new List<int>();
        }

        public Proizvod(int id, string ime, double cena, int kolicina, string opis, string slika, DateTime datum, string grad, List<int> listaRecenzija, bool statusDostupnosti)
        {
            this.id = id;
            this.cena = cena;
            this.kolicina = kolicina;
            this.opis = opis;
            this.slika = slika;
            this.datum = datum;
            this.grad = grad;
            this.listaRecenzija = listaRecenzija;
            this.statusDostupnosti = statusDostupnosti;
        }

        public int Id { get => id; set => id = value; }
        public string Ime { get => ime; set => ime = value; }
        public double Cena { get => cena; set => cena = value; }
        public int Kolicina { get => kolicina; set => kolicina = value; }
        public string Opis { get => opis; set => opis = value; }
        public string Slika { get => slika; set => slika = value; }
        public DateTime Datum { get => datum; set => datum = value; }
        public string Grad { get => grad; set => grad = value; }
        public List<int> ListaRecenzija { get => listaRecenzija; set => listaRecenzija = value; }
        public bool StatusDostupnosti { get => statusDostupnosti; set => statusDostupnosti = value; }
        public bool IsDeleted { get => isDeleted; set => isDeleted = value; }

        public override string ToString()
        {
            string listaRecenzija = string.Join(",", ListaRecenzija);

            return $"{Id},{Ime},{Cena},{Kolicina},{Opis},{Slika},{Datum},{Grad},{listaRecenzija},{StatusDostupnosti},{IsDeleted}";
        }

        public static Proizvod FromString(string input)
        {
            string[] vrednosti = input.Split(',');
            Proizvod proizvod = new Proizvod
            {
                id = int.Parse(vrednosti[0]),
                ime = vrednosti[1],
                cena = double.Parse(vrednosti[2]),
                kolicina = int.Parse(vrednosti[3]),
                opis = vrednosti[4],
                slika = vrednosti[5],
                datum = DateTime.Parse(vrednosti[6]),
                grad = vrednosti[7],
                listaRecenzija = vrednosti[8].Split(',').Select(int.Parse).ToList(),
                statusDostupnosti = bool.Parse(vrednosti[9]),
                isDeleted = bool.Parse(vrednosti[10])
            };

            return proizvod;
        }
    }
}