using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Web.Http;
using WebApp.Database;
using WebApp.Models;
using WebApp.Models.Bindings;

namespace WebApp.Controllers
{
    public class AccountController : ApiController
    {
        [HttpPost]
        [Route("api/account/register")]
        public IHttpActionResult Register(KupacBindingModel kupac)
        {
            if (BazaHelper.KorisnikPostoji(kupac.KorisnickoIme))
            {
                return BadRequest("Korisnicko ime postoji!");
            }

            DateTime parsedDateTime;
            DateTime.TryParseExact(kupac.DatumRodjenja, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out parsedDateTime);

            Kupac k = new Kupac
            {
                KorisnickoIme = kupac.KorisnickoIme,
                Lozinka = kupac.Lozinka,
                Ime = kupac.Ime,
                Prezime = kupac.Prezime,
                DatumRodjenja = parsedDateTime,
                Email = kupac.Email,
                Pol = (Pol)Enum.Parse(typeof(Pol), kupac.Pol),
                Uloga = Uloga.Kupac,
                OmiljeniProizvodi = new List<int>(),
                Porudzbine = new List<int>()
            };

            Baza.korisnici.Add(k.KorisnickoIme, k);

            return Ok("Registracija uspjesna!");
        }

        [HttpPost]
        [Route("api/account/login")]
        public IHttpActionResult Login(LoginModel model)
        {
            // Authenticate user based on the credentials
            // Replace with your own authentication logic

            string username = model.Username;
            string password = model.Password;

            // Check username and password against your user database
            bool isValid = AuthenticateUser(username, password);

            if (isValid)
            {
                // Get user type based on username
                string userType = GetUserType(username);

                // Generate JWT token
                string token = GenerateJwtToken(username, userType);

                // Return the token and user type as a response
                return Ok(new { token, userType });
            }

            // Return unauthorized status if authentication fails
            return Unauthorized();
        }

        private bool AuthenticateUser(string username, string password)
        {
            Korisnik k;

            if (Baza.korisnici.ContainsKey(username))
            {
                Baza.korisnici.TryGetValue(username, out k);
                if(k.Lozinka == password)
                {
                    return true;
                }
            }

            return false;
        }

        private string GetUserType(string username)
        {
            Korisnik k;
            Baza.korisnici.TryGetValue(username, out k);

            if (k.Uloga == Uloga.Kupac)
            {
                return "Kupac";
            }
            else if (k.Uloga == Uloga.Prodavac)
            {
                return "Prodavac";
            }
            else
            {
                return "Administrator";
            }
        }

        private string GenerateJwtToken(string username, string userType)
        {
            // Generate token expiration time
            DateTime expiryDate = DateTime.UtcNow.AddHours(2);

            // Create claims for the token
            var claims = new List<Claim>
            {
                new Claim("Name", username),
                new Claim("UserType", userType)
            };

            // Create the JWT token
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = expiryDate,
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes(GenerateRandomKey(32))),
                    SecurityAlgorithms.HmacSha256Signature
                )
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        private static string GenerateRandomKey(int keySize)
        {
            byte[] keyBytes = new byte[keySize];
            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(keyBytes);
            }
            return Convert.ToBase64String(keyBytes);
        }
    }
}


