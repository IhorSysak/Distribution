using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Distributor.WEB.Models
{
    public class TaskViewModel
    {
        public int TaskID { get; set; }
        [MaxLength(20, ErrorMessage = "Lenght can`t be more than 20")]
        [MinLength(2, ErrorMessage = "Lenght can`t be less than 2")]
        [Required(ErrorMessage = "Title is empty")]
        public string Title { get; set; }
        public DateTime Deadline { get; set; }
        public ICollection<ControlCenterViewModel> ControlCenterViewModels { get; set; }
    }
}