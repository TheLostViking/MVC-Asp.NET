using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using App.Models;

namespace App.ViewModels
{
    public class AddCourseViewModel
    {
        
        [Display(Name = "Course Number")]
        [Required(ErrorMessage = "Please enter course number!")]
        public int CourseNumber { get; set; }
        
        [Display(Name = "Course Title")]
        [Required(ErrorMessage = "You have to enter a title.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "You have to enter a description.")]
        public string Description { get; set; }

        [Display(Name = "Length(H)")]
        [Required(ErrorMessage = "You have to enter the length of the course.")]
        public string Length { get; set; }

        [Display(Name = "Level (Beginner, Intermediate or Advanced)")]
        [Required(ErrorMessage = "Please enter course level!")]
        // public int Level { get; set; } //Ändrad från string
        public string Level { get; set; }
        
        [Display(Name = "Price(USD) No decimals allowed.")]
        [Required(ErrorMessage = "You have to enter a price.")]
        public decimal? Price { get; set; }

        [NotMapped]
        public List<LevelModel> LevelCollection { get; set; }
    }
}