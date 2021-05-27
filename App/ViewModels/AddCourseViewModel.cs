using System.ComponentModel.DataAnnotations;

namespace App.ViewModels
{
    public class AddCourseViewModel
    {
        [Display(Name = "Course Title")]
        public string Title { get; set; }     
        public string Description { get; set; }
        public string Length { get; set; }
        public string Category { get; set; }
        [Display(Name = "Price(USD)")]
        public decimal Price { get; set; }        
    }
}