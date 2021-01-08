using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Form.Microservice.Models.Requests
{
    public class ContactFromGetRequest
    {
        [Required]
        public DateTime FromDate { get; set; }
        public DateTime? ToDate { get; set; }
    }
}
