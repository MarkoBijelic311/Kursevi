using Kursevi.Base;
using Kursevi.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
            ViewBag.HeaderFooter = true;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(Korisnici korisnici)
        {
            if (ModelState.IsValid)
            {
                var existingUser = await _context.Korisnici.FirstOrDefaultAsync(x => x.Email == korisnici.Email);

                if (existingUser != null)
                {
                    TempData["ErrorMessage"] = "Vec postoji osoba sa tim emailom";
                    return RedirectToAction("Login");
                }

                await _context.Korisnici.AddAsync(korisnici);
                _context.SaveChangesAsync();
                return View("About");
            }

            HttpContext.Session.SetString("CurrentUserEmail", korisnici.Email);
            return BadRequest();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string email,string password)
        {
            var something = await _context.Korisnici.FirstOrDefaultAsync(x=> x.Email == email);

            if(something == null)
            {
                TempData["ErrorMessage"] = $"{email} ne postoji.";
                return RedirectToAction("Login");
            }

            else if(something.Password != password)
            {
                TempData["ErrorMessage"] = $"Nije pravilna sifra za {email}";
                return RedirectToAction("Login");
            }

            HttpContext.Session.SetString("CurrentUserEmail", something.Email);
            return View("About");
        }

        [HttpPost]
        public async Task<IActionResult> SendCourse([FromBody]Programiranje programiranje)
        {
            var email = HttpContext.Session.GetString("CurrentUserEmail");
            var prog = await _context.Programiranjes.FirstOrDefaultAsync(x=>x.CourseId == programiranje.CourseId);
            if(ModelState.IsValid)
            {
                if(prog == null)
                {
                    await _context.Programiranjes.AddAsync(programiranje);
                    await _context.SaveChangesAsync();
                }

                KorisnikKurs korisnikKurs = new KorisnikKurs()
                {
                    KorisnikID = email,
                    KursID = programiranje.CourseId
                };

                await _context.KorisnikKurs.AddAsync(korisnikKurs);
                await _context.SaveChangesAsync();
            }
            else
            {
                return BadRequest("Nije proslo dalje");
            }

            return Ok("Course added to user");
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
