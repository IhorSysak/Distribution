using System.ComponentModel.DataAnnotations;


namespace Distributor.WEB.Models
{
    public class ControlCenterViewModel
    {
        public int ControlCenterID { get; set; }
        [Required(ErrorMessage = "TaskName is empty")]
        public int TaskID { get; set; }
        [MaxLength(20, ErrorMessage = "Lenght can`t be more than 20")]
        [MinLength(2, ErrorMessage = "Lenght can`t be less than 2")]
        [Required(ErrorMessage = "Title is empty")]
        public string TaskName { get; set; }
        [Required(ErrorMessage = "StudentName is empty")]
        public int StudentID { get; set; }
        [MaxLength(20, ErrorMessage = "Lenght can`t be more than 20")]
        [MinLength(2, ErrorMessage = "Lenght can`t be less than 2")]
        [Required(ErrorMessage = "Title is empty")]
        public string StudentName { get; set; }
        [MaxLength(20, ErrorMessage = "Lenght can`t be more than 20")]
        [MinLength(2, ErrorMessage = "Lenght can`t be less than 2")]
        [Required(ErrorMessage = "Status is empty")]
        public string Status { get; set; }
    }
}