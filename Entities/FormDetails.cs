using System;
using System.Collections.Generic;

namespace Form.Microservice.Entities
{
    public partial class FormDetails
    {
        public long Id { get; set; }
        public long FormId { get; set; }
        public long? AnswerFormDetailsId { get; set; }
        public string FieldName { get; set; }
        public string FieldValue { get; set; }
        public DateTime CreateDate { get; set; }

        public virtual Form Form { get; set; }
    }
}
