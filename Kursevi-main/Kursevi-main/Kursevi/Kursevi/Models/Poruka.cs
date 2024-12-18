using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Kursevi.Models
{
    public class Poruka
    {
        public int PorukaID { get; set; }
        public string? SadrzajPoruke { get; set; }
        public DateTime DatumSlanja { get; set; }

        [ForeignKey("Korisnik")]
        public int KorisnikID { get; set; }
        [JsonIgnore]
        public Korisnik Korisnik { get; set; }
    }

}
