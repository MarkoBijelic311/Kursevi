namespace Kursevi.Models
{
    public class Lesson
    {
        public int id_lesson { get; set; }
        public string Content { get; set; }
        public string LessonTitle {  get; set; }
        public int ModuleId { get; set; }
        public Module Module { get; set; }
        public ICollection<LessonProgress> Progress { get; set; }
    }
}
