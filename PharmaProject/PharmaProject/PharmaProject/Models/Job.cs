//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------


namespace PharmaProject.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.ComponentModel.DataAnnotations;
   
    public partial class Job
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Enter Your Name !")]
        [StringLength(maximumLength: 12, MinimumLength = 3, ErrorMessage = "Username length must be Max 12 & Min 3")]
        public string Name { get; set; }


        [Required(ErrorMessage = "Enter Father Name !")]
        [StringLength(maximumLength: 12, MinimumLength = 3, ErrorMessage = "Username length must be Max 12 & Min 3")]
        public string Father_Name { get; set; }

        [Required(ErrorMessage = "Please Enter the Email Address !")]        
        public string Email_Address { get; set; }

        [Required]
        [MaxLength(11)]
        [MinLength(1)]
        [RegularExpression("^[0-9]*$", ErrorMessage = "must be numeric")]
        public Nullable<int> Phone_number { get; set; }

        [Required(ErrorMessage = "Enter Your City !")]
        public string City { get; set; }
        [Required(ErrorMessage = "Enter Your State !")]
        public string State { get; set; }
        [Required(ErrorMessage = "Enter Your Date of birth !")]
        public string DOB { get; set; }
        [Required(ErrorMessage = "Enter Your Qualification !")]
        public string Qualification_ { get; set; }
        [Required(ErrorMessage = "Enter Your Education !")]
        public string Education { get; set; }
        [Required(ErrorMessage = "Upload Resume !")]
        public string Resume { get; set; }
        public HttpPostedFileBase Imageayi { get; set; }

    }
}
