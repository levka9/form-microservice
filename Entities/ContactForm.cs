using System;
using System.Collections.Generic;

namespace ContactForm.Microservice.Entities
{
    public partial class ContactForm
    {
        public ContactForm()
        {
            ContactFormDetails = new HashSet<ContactFormDetails>();
        }

        public long Id { get; set; }
        public string ContactFormType { get; set; }
        public string ApplicationName { get; set; }
        public string UserId { get; set; }
        public DateTime CreatedDate { get; set; }

        public virtual ICollection<ContactFormDetails> ContactFormDetails { get; set; }
    }
}
