using System;
using System.Collections.Generic;

namespace Form.Microservice.Entities
{
    public partial class FormType
    {
        public FormType()
        {
            Form = new HashSet<Form>();
        }

        public byte Id { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
        public DateTime CreateDate { get; set; }

        public virtual ICollection<Form> Form { get; set; }
    }
}
