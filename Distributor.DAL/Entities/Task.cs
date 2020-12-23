using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Distributor.DAL.Entities
{
    public class Task
    {
        private int id;
        private string title;
        private DateTime deadline;
        [Key]
        public int TaskID { get { return id; } set { id = value; } }
        [MaxLength(20, ErrorMessage = "Lenght can`t be more than 20")]
        [MinLength(2, ErrorMessage = "Lenght can`t be less than 2")]
        [Required(ErrorMessage = "Title is empty")]
        public string Title { get { return title; } set { title = value; } }
        public DateTime Deadline { get { return deadline; } set { deadline = value; } }
        public ICollection<ControlCenter> ControlCenters { get; set; }
    }
}
