using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web;
using System.Web.Http;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class ProdavciController : ApiController
    {
        [HttpGet]
        //[Route("api/prodavci")]
        public IHttpActionResult GetProizvodi(string username)
        {
            var currentUser = Baza.korisnici.Values.FirstOrDefault(u => u.KorisnickoIme == username) as Prodavac;

            var claimsIdentity = User.Identity as ClaimsIdentity;

            // Retrieve the name claim from the claims identity
            var nameClaim = claimsIdentity?.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name);


            if (currentUser == null)
            {
                // User not found or is not a seller
                return NotFound();
            }

            // Get all products from Baza.proizvodi
            var allProducts = Baza.proizvodi.Values.ToList();

            // Filter products by the ones created by the current user
            var userProducts = allProducts.Where(p => currentUser.ObjavljeniProizvodi.Contains(p.Id));

            return Ok(userProducts);
        }
    }
}
