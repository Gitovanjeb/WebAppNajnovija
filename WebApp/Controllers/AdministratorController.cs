using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApp.Database;
using WebApp.Models;
using WebApp.Models.Bindings;

namespace WebApp.Controllers
{
    public class AdministratorController : ApiController
    {
        [HttpPost]
        [Route("api/account/registerProdavac")]
        public IHttpActionResult Register([FromBody] ProdavacBindingModel model)
        {
            if (BazaHelper.KorisnikPostoji(model.KorisnickoIme))
            {
                return BadRequest("Korisnicko ime postoji!");
            }

            try
            {
                Prodavac prodavac = new Prodavac
                {
                    KorisnickoIme = model.KorisnickoIme,
                    Lozinka = model.Lozinka,
                    Ime = model.Ime,
                    Prezime = model.Prezime,
                    Pol = (Pol)Enum.Parse(typeof(Pol), model.Pol),
                    Email = model.Email,
                    DatumRodjenja = DateTime.ParseExact(model.DatumRodjenja, "yyyy-MM-dd", null)
                };

                Baza.korisnici.Add(prodavac.KorisnickoIme, prodavac);

                return Ok();
            }
            catch (Exception ex)
            {
                // Return an error response
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete]
        [Route("api/AdministratorController/Delete")]
        public IHttpActionResult Delete(string username)
        {
            Korisnik postojeciKorisnik;
            if (!Baza.korisnici.TryGetValue(username, out postojeciKorisnik))
            {
                return NotFound();
            }

            postojeciKorisnik.IsDeleted = true;
            return Ok();
        }
        /* [HttpGet]
         [Route("api/AdministratorController/Get")]

         public List<Korisnik> Get()
         {
             return Baza.korisnici.Values.ToList();
         }*/
        [HttpPut]
        [Route("api/AdministratorController/Put")]
        public IHttpActionResult Put(UpdateProfilModel updateKorisnika)
        {
            // Retrieve the user information from the JWT token's claims
            List<Korisnik> korisnici = Baza.korisnici.Values.ToList();
            Korisnik korisnik = korisnici.FirstOrDefault(u => u.KorisnickoIme == updateKorisnika.KorisnickoIme);
            if (korisnik == null)
            {
                return NotFound();
            }

            korisnik.Ime = !string.IsNullOrEmpty(updateKorisnika.Ime) ? updateKorisnika.Ime : korisnik.Ime;
            korisnik.Prezime = !string.IsNullOrEmpty(updateKorisnika.Prezime) ? updateKorisnika.Prezime : korisnik.Prezime;
            korisnik.Lozinka = !string.IsNullOrEmpty(updateKorisnika.Lozinka) ? updateKorisnika.Lozinka : korisnik.Lozinka;
            korisnik.Email = !string.IsNullOrEmpty(updateKorisnika.Email) ? updateKorisnika.Email : korisnik.Email;
            DateTime parsedDatum;
            if (DateTime.TryParse(updateKorisnika.DatumRodjenja, out parsedDatum))
            {
                korisnik.DatumRodjenja = parsedDatum;
            }

            // korisnik.DatumRodjenja = DateTime.Parse(updateKorisnika.DatumRodjenja  ?? updateKorisnika.DatumRodjenja);

            return Ok();
        }

        [HttpGet]
        [Route("api/AdministratorController/Search")]
        public IHttpActionResult Search(string KorisnickoIme = null, string Ime = null, string Prezime = null, string OdDatumRodjenja = null, string DoDatumRodjenja = null, string Uloga = null, string isDeleted = null, string Lozinka=null)
        {
            // Retrieve all products from the database
            List<Korisnik> sviKorisnici = Baza.korisnici.Values.ToList();

            // Apply filters based on search criteria
            List<Korisnik> filtriraniKorisnici = sviKorisnici;

            if (!string.IsNullOrEmpty(KorisnickoIme))
                filtriraniKorisnici = filtriraniKorisnici.Where(p => p.KorisnickoIme.Contains(KorisnickoIme)).ToList();

            if (!string.IsNullOrEmpty(Ime))
                filtriraniKorisnici = filtriraniKorisnici.Where(p => p.Ime.Contains(Ime)).ToList();

            if (Prezime != null)
                filtriraniKorisnici = filtriraniKorisnici.Where(p => p.Prezime.Contains(Prezime)).ToList();

            if (!string.IsNullOrEmpty(Lozinka))
                filtriraniKorisnici = filtriraniKorisnici.Where(p => p.Lozinka.Contains(Lozinka)).ToList();

            if (!string.IsNullOrEmpty(OdDatumRodjenja) && !string.IsNullOrEmpty(DoDatumRodjenja))
            {
                DateTime vrednostPrva = DateTime.ParseExact(OdDatumRodjenja, "yyyy-MM-dd", CultureInfo.InvariantCulture);
                DateTime vrednostDruga = DateTime.ParseExact(DoDatumRodjenja, "yyyy-MM-dd", CultureInfo.InvariantCulture);

                filtriraniKorisnici = filtriraniKorisnici.Where(p => p.DatumRodjenja >= vrednostPrva && p.DatumRodjenja <= vrednostDruga).ToList();

            }

            if (!string.IsNullOrEmpty(Uloga))
            {
                if (Enum.TryParse(Uloga, out Uloga uloga))
                {
                    filtriraniKorisnici = filtriraniKorisnici.Where(p => p.Uloga == uloga).ToList();
                }
            }
            bool isDeletedBool;
            if (bool.TryParse(isDeleted, out isDeletedBool)) { 

            filtriraniKorisnici = filtriraniKorisnici.Where(p => p.IsDeleted == isDeletedBool).ToList();
            }
            //filtriraniKorisnici = filtriraniKorisnici.Where(p => p.IsDeleted == isDeleted).ToList();

            return Ok(filtriraniKorisnici);
        }

        [HttpPost]
        [Route("api/proizvodi/dodajproizvod")]
        public IHttpActionResult DodajProizvod(ProizvodBindingModel proizvod)
        {
            if (proizvod == null)
            {
                return BadRequest("Nevalidni podaci.");
            }

            Proizvod p = new Proizvod
            {
                Ime = proizvod.Ime,
                Cena = proizvod.Cena,
                Kolicina = proizvod.Kolicina,
                Opis = proizvod.Opis,
                Slika = proizvod.Slika,
                Datum = DateTime.Now,
                Grad = proizvod.Grad,
                StatusDostupnosti = proizvod.Kolicina > 0
            };

            Baza.proizvodi.Add(p.Id, p);

            return Ok();
        }


    }
}
