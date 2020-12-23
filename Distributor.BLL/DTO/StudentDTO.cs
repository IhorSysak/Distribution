﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Distributor.BLL.DTO
{
    public class StudentDTO
    {
        [Key]
        public int ID { get; set; }
        [MaxLength(15, ErrorMessage = "Lenght can`t be more than 15")]
        [MinLength(2, ErrorMessage = "Lenght can`t be less than 2")]
        [Required(ErrorMessage = "LastName is empty")]
        public string LastName { get; set; }
        [MaxLength(15, ErrorMessage = "Lenght can`t be more than 15")]
        [MinLength(2, ErrorMessage = "Lenght can`t be less than 2")]
        [Required(ErrorMessage = "FirstMidName is empty")]
        public string FirstMidName { get; set; }
        public ICollection<ControlCenterDTO> ControlCenterDTOs { get; set; }
    }
}
