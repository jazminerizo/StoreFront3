using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace StoreFront3.UI.MVC.Models
{
    public class ContactViewModel
    {
        [Key]
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [EmailAddress]
        [Required(ErrorMessage = "Email is required")]
        //[DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "A subject is required")]
        public string Subject { get; set; }

        [Required(ErrorMessage = "Message is required")]
        [UIHint("MultilineText")]
        public string Message { get; set; }

    }
}