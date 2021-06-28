using System.Collections.Generic;

namespace Api.Entities
{
    public class Course
    {
        public int  Id { get; set; }
        public int CourseNumber { get; set; }
        public string Title {get; set; }
        public string Description { get; set; }
        public string Length { get; set; }
        public decimal Price { get; set; } 
        public virtual Status Status { get; set; }
        public virtual Level Level { get; set; }
        public ICollection<Student> Students { get; set; }
    }
}