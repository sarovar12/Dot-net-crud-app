using System.ComponentModel.DataAnnotations;

namespace CrudApp.Models
{
    public class Skills
    {
       

        [Key]
        public int skill_id { get; set; }
        public string skill_name { get; set; }
        public int information_id { get; set; }


    }
}
