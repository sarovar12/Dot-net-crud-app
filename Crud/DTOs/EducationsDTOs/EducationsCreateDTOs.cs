namespace Crud.DTOs.EducationsDTOs
{
    public class EducationsCreateDTOs
    {
 
        public string name { get; set; }
        public string degree_name { get; set; }
        public string board_name { get; set; }
        public string college_name { get; set; }
        public DateTime start_date { get; set; }
        public DateTime? end_date { get; set; }
        public int information_ID { get; set; }
    }
}
