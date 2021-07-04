namespace Api.ViewModels
{
    public class AddCourseViewModel
    {
        public int CourseNumber { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Length { get; set; }
        public string Level { get; set; }   //Ändrad från string
        public decimal Price { get; set; }     
        
    }
}