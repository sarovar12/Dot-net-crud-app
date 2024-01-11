using System.ComponentModel.DataAnnotations;

namespace Crud.DTOs.InformationDTOs
{
    public class InformationCreateDTOs
    {
        
        public string name { get; set; }
        public string summary { get; set; }
        public string email { get; set; }
        public string password { get; set; }
      
    }
}
