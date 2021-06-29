namespace App.Models
{
    public class CourseModel
    {
        public int  CourseId { get; set; }
        public int CourseNumber { get; set; }
        public string Title {get; set; }
        public string Description { get; set; }
        public string Length { get; set; }
        public string Level { get; set; }
        public decimal Price { get; set; } 
        public string Status { get; set; }
    }
}