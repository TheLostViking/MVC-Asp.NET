using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Api.Entities
{
    public class Course
    {
        public int  CourseId { get; set; }
        public int CourseNumber { get; set; }
        public string Title {get; set; }
        public string Description { get; set; }
        public string Length { get; set; }
        public decimal Price { get; set; } 
        public int LevelId { get; set; }
        public int StatusId { get; set; }


        [ForeignKey("StatusID")]
        public virtual Status Status { get; set; }
        
        [ForeignKey("LevelID")]
        public virtual Level Level { get; set; }
        public ICollection<CourseStudent> CourseStudents { get; set; }
    }
}