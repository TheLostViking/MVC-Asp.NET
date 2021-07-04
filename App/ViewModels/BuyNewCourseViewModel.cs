using System.ComponentModel.DataAnnotations;

namespace App.ViewModels
{
    public class BuyNewCourseViewModel
    {
        public int CourseId { get; set; }
        public int CourseNumber { get; set; }
        public string Title { get; set; }        
        public string Description { get; set; }
        
        public string Length { get; set; }

        [Display(Name = "Price (USD)")]        
        public decimal Price { get; set; }        
        public int StudentId { get; set; }

        [Display(Name = "First Name")]
        public string FirstName { get; set; }        

        [Display(Name = "Last Name")]
        public string LastName { get; set; }        
        
    }
}