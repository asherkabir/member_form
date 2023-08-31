using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Member_Form.Models
{
    public class Student
    {
        public string Name { get; set; }
        [Required]
        [RegularExpression(@"\+?[0-9]{10}",
          ErrorMessage = "Entered phone format is not valid.")]
        public string PhoneNumber { get; set; }

        public string Gender { get; set; }

        public string Address { get; set; }
        public string Photo { get; set; }
    }
}