using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Kursevi.Models
{
    public class PrijavaNaKurs
    {
        public DateTime DatumPrijave { get; set; }

        [ForeignKey("Korisnik")]
        public int KorisnikID { get; set; }
        [JsonIgnore]
        public Korisnik Korisnik { get; set; }

        [ForeignKey("Kurs")]
        public int KursID { get; set; }
        [JsonIgnore]
        public Kurs Kurs { get; set; }

    }
}
