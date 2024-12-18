namespace TestKursevi;
using Xunit;
using Moq;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using Kursevi.Controllers; 
using Kursevi.Models;
using Kursevi.Base;
using Microsoft.AspNetCore.Mvc;

public class HomeControllerTests
{
    private readonly HomeController _controller;
    private readonly DbContextOptions<AppDbContextV2> _options;
    private readonly AppDbContextV2 _context;

    public HomeControllerTests()
    {
        _options = new DbContextOptionsBuilder<AppDbContextV2>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;

        _context = new AppDbContextV2(_options);

        _controller = new HomeController(_context);
    }

    [Fact]
    public void Team_Should_Add_New_Korisnik_To_Database()
    {
        var korisnik = new Korisnik
        {
            Ime = "Marko",
            Prezime = "Markovi?",
            Email = "marko@example.com",
            BrojTelefona = "+381641234567",
            Poruke = new List<Poruka>
            {
                new Poruka { SadrzajPoruke = "Prijava za kurs 1" },
                new Poruka { SadrzajPoruke = "Prijava za kurs 2" }
            },
            PrijaveNaKurseve = new List<PrijavaNaKurs>
            {
                new PrijavaNaKurs { KursID = 101 },
                new PrijavaNaKurs { KursID = 102 }
            }
        };
        var result = _controller.Team(korisnik);
        var korisnikIzBaze = _context.Korisniks.FirstOrDefault(k => k.Email == "marko@example.com");

        Assert.NotNull(korisnikIzBaze); 
        Assert.Equal("Marko", korisnikIzBaze.Ime); 
        Assert.Equal(2, korisnikIzBaze.Poruke.Count); 
        Assert.Equal(2, korisnikIzBaze.PrijaveNaKurseve.Count);  
    }

    [Fact]
    public void Team_Should_Not_Add_Duplicate_PrijavaNaKurs()
    {
        var korisnik = new Korisnik
        {
            Ime = "Jovan",
            Prezime = "Jovanovi?",
            Email = "jovan@example.com",
            BrojTelefona = "+381641234568",
            Poruke = new List<Poruka>
            {
                new Poruka { SadrzajPoruke = "Prijava za kurs 1" }
            },
            PrijaveNaKurseve = new List<PrijavaNaKurs>
            {
                new PrijavaNaKurs { KursID = 201 },
                new PrijavaNaKurs { KursID = 201 } 
            }
        };
        var result = _controller.Team(korisnik);

        var korisnikIzBaze = _context.Korisniks.FirstOrDefault(k => k.Email == "jovan@example.com");

        Assert.NotNull(korisnikIzBaze);
        Assert.Equal(1, korisnikIzBaze.PrijaveNaKurseve.Count);
    }

    [Fact]
    public void Team_Should_Return_Ok_Result()
    {
        var korisnik = new Korisnik
        {
            Ime = "Ana",
            Prezime = "Ani?",
            Email = "ana@example.com",
            BrojTelefona = "+381641234569",
            Poruke = new List<Poruka>
            {
                new Poruka { SadrzajPoruke = "Poruka za prijavu" }
            },
            PrijaveNaKurseve = new List<PrijavaNaKurs>
            {
                new PrijavaNaKurs { KursID = 301 }
            }
        };
        var result = _controller.Team(korisnik);

        Assert.IsType<OkObjectResult>(result);
    }
}
