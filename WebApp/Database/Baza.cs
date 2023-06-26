using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Web;
using WebApp.Models;

namespace WebApp.Models
{
    public static class Baza
    {
        #region Local Storage
        public static Dictionary<string, Korisnik> korisnici = new Dictionary<string, Korisnik>();
        public static Dictionary<int, Proizvod> proizvodi = new Dictionary<int, Proizvod>();
        public static Dictionary<int, Porudzbina> porudzbine = new Dictionary<int, Porudzbina>();
        public static Dictionary<int, Recenzija> recenzije = new Dictionary<int, Recenzija>();
        public static bool isChanged = true;
        #endregion

        #region Helpers
        public static void Run()
        {
            new Thread(() =>
            {
                Thread.CurrentThread.IsBackground = true;
                string line;
                var fullPath = System.Web.Hosting.HostingEnvironment.MapPath(@"~\Database\Podaci\korisnici.txt");
                using (System.IO.TextReader readFile = new StreamReader(fullPath))
                {
                    Korisnik k = null;
                    while (true)
                    {
                        line = readFile.ReadLine();
                        if (line == null)
                        {
                            break;
                        }
                        if (line.Split(',')[7] == "Prodavac")
                            k = Prodavac.Parse(line);
                        else if (line.Split(',')[7] == "Kupac")
                            k = Kupac.Parse(line);
                        else
                            k = Korisnik.Parse(line);

                        korisnici.Add(k.KorisnickoIme, k);
                    }
                }

                fullPath = System.Web.Hosting.HostingEnvironment.MapPath(@"~\Database\Podaci\proizvodi.txt");
                using (System.IO.TextReader readFile = new StreamReader(fullPath))
                {
                    while (true)
                    {
                        line = readFile.ReadLine();
                        if (line == null)
                        {
                            break;
                        }
                        Proizvod k = Proizvod.FromString(line);
                        proizvodi.Add(k.Id, k);
                    }
                }

                fullPath = System.Web.Hosting.HostingEnvironment.MapPath(@"~\Database\Podaci\recenzije.txt");
                using (System.IO.TextReader readFile = new StreamReader(fullPath))
                {
                    while (true)
                    {
                        line = readFile.ReadLine();
                        if (line == null)
                        {
                            break;
                        }
                        Recenzija k = Recenzija.FromString(line);
                        recenzije.Add(k.Id, k);
                    }
                }

                fullPath = System.Web.Hosting.HostingEnvironment.MapPath(@"~\Database\Podaci\porudzbine.txt");
                using (System.IO.TextReader readFile = new StreamReader(fullPath))
                {
                    while (true)
                    {
                        line = readFile.ReadLine();
                        if (line == null)
                        {
                            break;
                        }
                        Porudzbina k = Porudzbina.FromString(line);
                        porudzbine.Add(k.Id, k);
                    }
                }

                while (true)
                {
                    if (isChanged)
                    {
                        fullPath = System.Web.Hosting.HostingEnvironment.MapPath(@"~\Database\Podaci\korisnici.txt");
                        using (TextWriter tw = new StreamWriter(fullPath))
                        {
                            foreach (var item in korisnici.Values.ToList())
                            {
                                tw.WriteLine(item.ToString());
                            }
                        }

                        fullPath = System.Web.Hosting.HostingEnvironment.MapPath(@"~\Database\Podaci\proizvodi.txt");
                        using (TextWriter tw = new StreamWriter(fullPath))
                        {
                            foreach (var item in proizvodi.Values.ToList())
                            {
                                tw.WriteLine(item.ToString());
                            }
                        }

                        fullPath = System.Web.Hosting.HostingEnvironment.MapPath(@"~\Database\Podaci\recenzije.txt");
                        using (TextWriter tw = new StreamWriter(fullPath))
                        {
                            foreach (var item in recenzije.Values.ToList())
                            {
                                tw.WriteLine(item.ToString());
                            }
                        }

                        fullPath = System.Web.Hosting.HostingEnvironment.MapPath(@"~\Database\Podaci\porudzbine.txt");
                        using (TextWriter tw = new StreamWriter(fullPath))
                        {
                            foreach (var item in porudzbine.Values.ToList())
                            {
                                tw.WriteLine(item.ToString());
                            }
                        }

                    }
                    Thread.Sleep(10000);
                }
            }).Start();
        }
        #endregion
    }
}