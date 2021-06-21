using System.ComponentModel.DataAnnotations;

namespace App.ViewModels
{
    public class AddCourseViewModel
    {
        [Display(Name = "Course Title")]
        [Required(ErrorMessage = "You have to enter a title.")]
        public string Title { get; set; }     
        public string CourseNumber { get; set; }
        public string Description { get; set; }
        [Display(Name = "Length(H)")]
        public string Length { get; set; }
        public string Level { get; set; }
        [Display(Name = "Price(USD)")]
        [Required(ErrorMessage = "You have to enter a price.")]
        public decimal? Price { get; set; }        
    }
}