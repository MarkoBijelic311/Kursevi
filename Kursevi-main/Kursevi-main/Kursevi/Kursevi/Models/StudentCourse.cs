namespace Kursevi.Models
{
    public class StudentCourse
    {
        public int StudentId { get; set; }
        public int CourseId { get; set; }
        public Users User { get; set; } 
        public Course Course { get; set; }
    }
}
