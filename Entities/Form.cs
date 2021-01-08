using System;
using System.Collections.Generic;

namespace Form.Microservice.Entities
{
    public partial class Form
    {
        public Form()
        {
            FormDetails = new HashSet<FormDetails>();
        }

        public long Id { get; set; }
        public byte FormTypeId { get; set; }
        public string ApplicationName { get; set; }
        public string UserId { get; set; }
        public DateTime CreatedDate { get; set; }

        public virtual FormType FormType { get; set; }
        public virtual ICollection<FormDetails> FormDetails { get; set; }
    }
}
