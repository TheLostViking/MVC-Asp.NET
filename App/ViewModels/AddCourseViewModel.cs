using System.ComponentModel.DataAnnotations;
using App.Models;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace App.ViewModels
{
    public class AddCourseViewModel
    {
        [Display(Name = "Course Title")]
        [Required(ErrorMessage = "You have to enter a title.")]
        public string Title { get; set; } 

        [Display(Name = "Course Number")]
        [Required(ErrorMessage = "Please enter course number!")]
        public int CourseNumber { get; set; }
        
        [Required(ErrorMessage = "You have to enter a description.")]
        public string Description { get; set; }

        [Display(Name = "Length(H)")]
        [Required(ErrorMessage = "You have to enter the length of the course.")]
        public string Length { get; set; }

        [Required(ErrorMessage = "Please enter course level!")]
        public string Level { get; set; }
        
        [Display(Name = "Price(USD) No decimals allowed.")]
        [Required(ErrorMessage = "You have to enter a price.")]
        public decimal? Price { get; set; }        
    }
}