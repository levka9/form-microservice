using System;
using System.Collections.Generic;

namespace ContactForm.Microservice.Entities
{
    public partial class ContactFormDetails
    {
        public long Id { get; set; }
        public long ContactFormId { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Subjeсt { get; set; }
        public string Message { get; set; }
        public DateTime CreateDate { get; set; }

        public virtual ContactForm ContactForm { get; set; }
    }
}
