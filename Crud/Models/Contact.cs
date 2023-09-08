using System.ComponentModel.DataAnnotations;

namespace CrudApp.Models
{
    public class Contact
    {
        [Key]
        public int contact_id { get; set; }
        public string contact_name { get; set; }
        public string contact_link { get; set; }
        public int information_ID { get; set; }
    }
}
