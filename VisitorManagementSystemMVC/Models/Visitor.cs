using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace VisitorManagementSystemMVC.Models
{
    public class Visitor
    {
        [Key]
        public int Id { get; set; }


        [StringLength(50, MinimumLength = 3)]
        [DisplayName("Visitor Name")]
        [Required(ErrorMessage = "Visitor Name is required")]
        public string Name { get; set; }

        [Required]
        [StringLength(10, ErrorMessage = "Phone number cannot be longer than 10 digits.")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Please enter your email address")]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email address")]
        [MaxLength(50)]
        [RegularExpression(@"[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,4}", ErrorMessage = "Please enter correct email")]
        public string Email { get; set; }


        [Required(ErrorMessage = "Please choose profile picture")]
        public string ProfilePicture { get; set; }

        [Required(ErrorMessage = "Please choose date")]
        [Display(Name = "Visited Date")]
        [DataType(DataType.Date)]
        public DateTime VisitedDate { get; set; }

        public bool Approval { get; set; }
        public bool Rejected { get; set; }

    }
}

