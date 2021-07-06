using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Api.Entities
{
    public class Status
    {
        public int Id { get; set; }        
        public string Name { get; set; }
        public virtual ICollection<Course> Courses { get; set; }
    }
}