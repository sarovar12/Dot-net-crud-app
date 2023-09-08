using System.ComponentModel.DataAnnotations;

namespace CrudApp.Models
{
    public class Information
    {
        [Key]
        public int information_id { get; set; }
        public string name { get; set; }
        public string summary { get; set; }
        public string email { get; set; }
        public string password { get; set; }

    }



}




