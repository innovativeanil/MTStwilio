using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TwilioPOC.Models
{
    public class Feedback
    {
        public int Id { get; set; }
        public string Submitter { get; set; }
        public string Phone { get; set; }
        public string Message { get; set; }
        public string Status { get; set; }
    }
}