using Kursevi.Base;
using Kursevi.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Kursevi.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContextV2 _context;

        public HomeController(AppDbContextV2 context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Team()
        {
            return View();
        }

        [HttpPost]
        [Route("/Home/Team")]
        public IActionResult Team([FromBody] Korisnik korisnik)
        {
            var korisnikEntitet = new Korisnik
            {
                Ime = korisnik.Ime,
                Prezime = korisnik.Prezime,
                BrojTelefona = korisnik.BrojTelefona,
                Email = korisnik.Email,
            };

            foreach (var poruka in korisnik.Poruke)
            {
                korisnikEntitet.Poruke.Add(new Poruka
                {
                    SadrzajPoruke = poruka.SadrzajPoruke,
                    DatumSlanja = DateTime.Now
                });
            }

            foreach (var prijava in korisnik.PrijaveNaKurseve)
            {
                var vecPostoji = korisnikEntitet.PrijaveNaKurseve
                    .Any(p => p.KursID == prijava.KursID);

                if (!vecPostoji)
                {
                    korisnikEntitet.PrijaveNaKurseve.Add(new PrijavaNaKurs
                    {
                        DatumPrijave = DateTime.Now,
                        KursID = prijava.KursID
                    });
                }
            }

            _context.Korisniks.Add(korisnikEntitet);
            _context.SaveChanges();

            return Ok(korisnikEntitet);
        }
    }
}
