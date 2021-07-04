using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using App.Models;

namespace App.ViewModels
{
    public class DetailStudentCourseViewModel
    {
        public int StudentId { get; set; }

        [Display(Name="First Name")]
        public string FirstName { get; set; }

        [Display(Name="Last Name")]
        public string LastName { get; set; }
        public string Phone { get; set; } 
        public string Email { get; set; }
        public string Adress { get ; set; }

        public virtual ICollection<CourseModel> Courses { get; set; }
        public virtual ICollection<CourseModel> AvailableCourses { get; set; }

    }
}