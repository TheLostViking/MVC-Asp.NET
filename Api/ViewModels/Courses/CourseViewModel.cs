using System.Collections.Generic;
using System.Text.Json.Serialization;
using Api.ViewModels.Students;

namespace Api.ViewModels
{
    public class CourseViewModel
    {
        public int  CourseId { get; set; }
        public int CourseNumber { get; set; }
        public string Title {get; set; }
        public string Description { get; set; }
        public string Length { get; set; }
        public string Level { get; set; }
        public decimal Price { get; set; } 
        public string Status { get; set; }        
        public virtual ICollection<StudentViewModel> Students { get; set; }
    }
}