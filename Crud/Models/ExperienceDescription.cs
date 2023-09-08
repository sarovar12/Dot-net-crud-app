using System.ComponentModel.DataAnnotations;

namespace Crud.Models
{
    public class ExperienceDescription
    {
        [Key]
        public int expdescription_id { get; set; }
        public string expdescription_text { get; set; }
        public int experience_id { get; set; }
    }
}

