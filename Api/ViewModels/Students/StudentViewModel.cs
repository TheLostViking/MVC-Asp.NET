using System.Collections.Generic;

namespace Api.ViewModels.Students
{
    public class StudentViewModel
    {
        public int StudentId { get; set; }        
        public string FirstName { get; set; } 
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Adress { get; set; }
        public virtual ICollection<CourseViewModel> Courses { get; set; }
    }
}