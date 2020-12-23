using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Distributor.DAL.Entities
{
    public class Student
    {
        private int id;
        private string lastName;
        private string firstMidName;
        [Key]
        public int ID { get { return id; } set { id = value; } }
        [MaxLength(15, ErrorMessage = "Lenght can`t be more than 15")]
        [MinLength(2, ErrorMessage = "Lenght can`t be less than 2")]
        [Required(ErrorMessage = "LastName is empty")]
        public string LastName { get { return lastName; } set { lastName = value; } }
        [MaxLength(15, ErrorMessage = "Lenght can`t be more than 15")]
        [MinLength(2, ErrorMessage = "Lenght can`t be less than 2")]
        [Required(ErrorMessage = "FirstMidName is empty")]
        public string FirstMidName { get { return firstMidName; } set { firstMidName = value; } }
        public ICollection<ControlCenter> ControlCenters { get; set; }
    }
}
