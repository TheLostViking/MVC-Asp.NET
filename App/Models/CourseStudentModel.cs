namespace App.Models
{
    public class CourseStudentModel
    {
        public int StudentId { get; set; }
        public int CourseId { get; set; }
        public virtual CourseModel Course { get; set; }
        public virtual StudentModel Student { get; set; }   
    }
}