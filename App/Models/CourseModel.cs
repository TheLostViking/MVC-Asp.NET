using System.Collections.Generic;
using System.Text.Json.Serialization;
using App.Data;

namespace App.Models
{
    public class CourseModel
    {        
        public int  CourseId { get; set; }
        public int CourseNumber { get; set; }
        public string Title {get; set; }
        public string Description { get; set; }
        public string Length { get; set; }
        //public int LevelId { get; set; } //Ändrad från string som test.
        public string Level { get; set; }
        public int StatusId { get; set;}
        //public string Status { get; set;}
        public decimal Price { get; set; }     

        public virtual ICollection<StudentModel> Students { get; set;}    
   }
}