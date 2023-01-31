using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NextHome.Models
{
    public class RequestModel
    {
        [Required]
        public string FullName { get; set; }
        [Required]
        public string MobileNo { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public string ProjectName { get; set; }
        [Required]
        public string Message { get; set; }
        
        //this property is trap for bot requests
        public string PhoneNo { get; set; }
    }
}