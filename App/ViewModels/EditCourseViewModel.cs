using System.ComponentModel.DataAnnotations;

namespace App.ViewModels
{
    public class EditCourseViewModel
    {
        public int Id { get; set; }        

        [Display(Name = "Course Number")]
        [Required(ErrorMessage = "Course Number can't be empty!")]
        public int CourseNumber { get; set; }

        [Display(Name = "Course Title")]
        [Required(ErrorMessage = "You must enter a title!")]
        public string Title { get; set; }             

        [Required(ErrorMessage = "Description can't be empty!")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Course length is required.")]
        public string Length { get; set; }
        
        [Required(ErrorMessage = "Course Level is required")]
        public string Level { get; set;} 
        
        [Display(Name = "Price (USD) No decimals allowed")]
        [Required(ErrorMessage = "Price can't be empty.")]
        public decimal Price { get; set; }        
    }
    
}