using System.Collections.Generic;

namespace Api.Entities
{
    public class Level
    {
        public int Id { get; set; }        
        public string Name { get; set; }       
        public virtual ICollection<Course> Courses { get; set; }
    }
}