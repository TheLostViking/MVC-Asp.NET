namespace App.Entities
{
    public class Course
    {      
        public int  Id { get; set; }
        public int CourseNumber { get; set; }
        public string Title {get; set; }
        public string Description { get; set; }
        public string Length { get; set; }
        public string Level { get; set; }
        public decimal Price { get; set; } 
        public bool Active { get; set; }
    }
}