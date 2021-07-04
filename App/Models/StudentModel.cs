using System.Collections.Generic;

namespace App.Models
{
    public class StudentModel
    {
        public int StudentId { get; set; }        
        public string FirstName { get; set; }        
        public string LastName { get; set; }        
        public string Email { get; set; }        
        public string Phone { get; set; }        
        public string Adress { get; set; }     

        public virtual ICollection<CourseModel> Courses { get; set; }
        
    }
}