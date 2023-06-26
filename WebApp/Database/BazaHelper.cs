using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApp.Models;

namespace WebApp.Database
{
    public static class BazaHelper
    {
        public static bool KorisnikPostoji(string korisnickoIme)
        {
            return Baza.korisnici.ContainsKey(korisnickoIme);
        }
    }
}