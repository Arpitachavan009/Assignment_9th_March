using Humanizer;
using Mono.TextTemplating;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Diagnostics.Metrics;

namespace Assignments_9th_March.Models
{
    public class Employee
    {
        [Key]
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }

        [Display(Name = "Department Name")]
        public string Department { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        [Display(Name = "Pin Code")]
        public int Pin_Code { get; set; }
        public string Hobbies { get; set; }





    }



}
