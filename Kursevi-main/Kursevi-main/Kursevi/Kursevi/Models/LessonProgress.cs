using static Kursevi.Models.Users;

namespace Kursevi.Models
{
    public class LessonProgress
    {
        public int UserId { get; set; }
        public int LessonId { get; set; }
        public bool IsCompleted { get; set; }
        public Users Student { get; set; }
        public Lesson Lesson { get; set; }
    }
}
