namespace Kursevi.Models
{
    public class Users
    {
        public int User_Id { get; set; }
        public string Name { get; set; }
        public ICollection<Course> Courses { get; set; }
        public ICollection<StudentCourse> StudentCourses { get; set; }
        public ICollection<LessonProgress> LessonProgresses { get; set; }
    }
}
