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
using Microsoft.AspNetCore.Http;

public class HomeControllerTests
{
    private readonly HomeController _controller;
    private readonly DbContextOptions<AppDbContextV2> _options;
    private readonly AppDbContextV2 _context;

    public HomeControllerTests()
    {
        _options = new DbContextOptionsBuilder<AppDbContextV2>()
            .UseMySql("Server=localhost;database=kurs;user=root;password=''", new MySqlServerVersion(new Version(8, 0, 33)))
            .Options;

        _context = new AppDbContextV2(_options);
        _controller = new HomeController(_context)
        {
            ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext()
            }
        };

        _controller.HttpContext.Session = new MockSession();
    }

    [Fact]
    public async Task Register_Should_Add_New_User_To_Database()
    {
        var korisnici = new Korisnici
        {
            Email = "test@example.com",
            Password = "password",
            Ime = "Test",
            Prezime = "Testovic",
            Student = "true",
            Zaposlen = "true"
        };

        var result = await _controller.Register(korisnici);
        var korisnikIzBaze = _context.Korisnici.FirstOrDefault(k => k.Email == "test@example.com");

        Assert.NotNull(korisnikIzBaze);
        Assert.Equal("test@example.com", korisnikIzBaze.Email);
        Assert.IsType<ViewResult>(result);
    }

    [Fact]
    public async Task Register_Should_Not_Add_Existing_User()
    {
        _context.Korisnici.Add(new Korisnici 
        { 
            Email = "drugiTest@example.com",
            Password = "nesto",
            Ime = "Test",
            Prezime = "Testovic",
            Student = "true",
            Zaposlen = "true"
        });
        _context.SaveChanges();

        var korisnici = new Korisnici { 
            Email = "drugiTest@example.com",
            Password = "drugo",
            Ime = "Test",
            Prezime = "Testovic",
            Student = "true",
            Zaposlen = "true"
        };

        var result = await _controller.Register(korisnici);
        var redirectResult = Assert.IsType<RedirectToActionResult>(result);

        Assert.Equal("Login", redirectResult.ActionName);
    }

    [Fact]
    public async Task Login_Should_Return_View_For_Valid_Credentials()
    {
        _controller.HttpContext.Session.SetString("CurrentUserEmail", "test@example.com");
        var result = await _controller.Login("test@example.com", "password");
        Assert.IsType<ViewResult>(result);
    }

    [Fact]
    public async Task Login_Should_Redirect_For_Invalid_Credentials()
    {
        var result = await _controller.Login("test@example.com", "wrongpassword");
        var redirectResult = Assert.IsType<RedirectToActionResult>(result);
        
        Assert.Equal("Login", redirectResult.ActionName);
    }

    [Fact]
    public async Task SendCourse_Should_Add_New_Course()
    {
        _controller.HttpContext.Session.SetString("CurrentUserEmail", "test@example.com");

        var programiranje = new Programiranje { CourseId = 5, Name = "HTML/CSS osnove" };

        var result = await _controller.SendCourse(programiranje);
        var progIzBaze = _context.Programiranjes.FirstOrDefault(p => p.CourseId == 5);

        Assert.NotNull(progIzBaze);
        Assert.Equal("HTML/CSS osnove", progIzBaze.Name);
        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task SendCourse_Should_Not_Add_Duplicate_Course()
    {
        _context.Programiranjes.Add(new Programiranje { CourseId = 5, Name = "HTML/CSS osnove" });
        _context.SaveChanges();

        _controller.HttpContext.Session.SetString("CurrentUserEmail", "test@example.com");

        var programiranje = new Programiranje { CourseId = 5, Name = "HTML/CSS osnove" };

        var result = await _controller.SendCourse(programiranje);
        var progIzBaze = _context.Programiranjes.Where(p => p.CourseId == 5).ToList();

        Assert.Single(progIzBaze);
        Assert.IsType<OkObjectResult>(result);
    }
}
