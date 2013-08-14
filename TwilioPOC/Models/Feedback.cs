using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TwilioPOC.Models
{
    public class Feedback
    {
        [Required]
        public string Submitter { get; set; }
        [Required]
        public string Phone { get; set; }
        [Required]
        public string Message { get; set; }

        public int Id { get; set; }
        public string Status { get; set; }
    }
}