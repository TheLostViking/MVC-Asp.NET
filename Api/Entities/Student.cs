using System.Collections.Generic;

namespace Api.Entities
{
    public class Student
    {
        public int StudentId { get; set; }        
        public string FirstName { get; set; } 
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Adress { get; set; }
        public virtual ICollection<CourseStudent> CourseStudents { get; set; }
    }
}