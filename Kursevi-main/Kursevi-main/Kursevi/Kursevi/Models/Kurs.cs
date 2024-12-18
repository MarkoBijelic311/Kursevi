using System.Text.Json.Serialization;

namespace Kursevi.Models
{
    public class Kurs
    {
        public int KursID { get; set; }
        public string? NazivKursa { get; set; }
        public ICollection<PrijavaNaKurs> PrijaveNaKurseve { get; set; }
    }
}
