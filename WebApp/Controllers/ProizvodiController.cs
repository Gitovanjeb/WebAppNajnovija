using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;
using System.Web.Http.Description;
using WebApp.Models;
using WebApp.Models.Bindings;

namespace WebApp.Controllers
{
    public class ProizvodiController : ApiController
    {
        [HttpGet]
        public List<Proizvod> Get()
        {
            return Baza.proizvodi.Values.ToList();
        }

        [HttpGet]
        [Route("api/proizvodi/search")]
        public IHttpActionResult Search(string name = null, double? minPrice = null, double? maxPrice = null, string city = null, string sortBy = null)
        {
            // Retrieve all products from the database
            List<Proizvod> allProducts = Baza.proizvodi.Values.ToList();

            // Apply filters based on search criteria
            List<Proizvod> filteredProducts = allProducts;

            if (!string.IsNullOrEmpty(name))
                filteredProducts = filteredProducts.Where(p => p.Ime.Contains(name)).ToList();

            if (minPrice != null)
                filteredProducts = filteredProducts.Where(p => p.Cena >= minPrice.Value).ToList();

            if (maxPrice != null)
                filteredProducts = filteredProducts.Where(p => p.Cena <= maxPrice.Value).ToList();

            if (!string.IsNullOrEmpty(city))
                filteredProducts = filteredProducts.Where(p => p.Grad == city).ToList();

            // Sort the filtered products based on the provided sortBy parameter
            if (!string.IsNullOrEmpty(sortBy))
            {
                switch (sortBy.ToLower())
                {
                    case "nameasc":
                        filteredProducts = filteredProducts.OrderBy(p => p.Ime).ToList();
                        break;
                    case "namedesc":
                        filteredProducts = filteredProducts.OrderByDescending(p => p.Ime).ToList();
                        break;
                    case "priceasc":
                        filteredProducts = filteredProducts.OrderBy(p => p.Cena).ToList();
                        break;
                    case "pricedesc":
                        filteredProducts = filteredProducts.OrderByDescending(p => p.Cena).ToList();
                        break;
                    case "dateasc":
                        filteredProducts = filteredProducts.OrderBy(p => p.Datum).ToList();
                        break;
                    case "datedesc":
                        filteredProducts = filteredProducts.OrderByDescending(p => p.Datum).ToList();
                        break;
                    default:
                        break;
                }
            }

            return Ok(filteredProducts);
        }

        [HttpPost]
        [Route("api/proizvodi/dodajproizvod")]
        public IHttpActionResult DodajProizvod(ProizvodBindingModel proizvod)
        {
            if (proizvod== null)
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
