using System.ComponentModel.DataAnnotations;

namespace Kursevi.Models
{
    public class Korisnik
    {

        public int KorisnikID { get; set; }

        [Required]
        public string Ime { get; set; }

        [Required]
        public string Prezime { get; set; }

        [Required]
        public string BrojTelefona { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public ICollection<Poruka> Poruke { get; set; } = new List<Poruka>();

        [Required]
        public ICollection<PrijavaNaKurs> PrijaveNaKurseve { get; set; } = new List<PrijavaNaKurs>();
    }

}
