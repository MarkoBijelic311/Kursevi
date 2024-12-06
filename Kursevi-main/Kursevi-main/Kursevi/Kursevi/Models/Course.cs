using static Kursevi.Models.Users;

namespace Kursevi.Models
{
    public class Course
    {
        public int id_course { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int UserId { get; set; }
        public Users User { get; set; }
        public ICollection<Module> Modules { get; set; }
        public ICollection<StudentCourse> StudentCourses { get; set; }
    }
}
