using Form.Microservice.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Form.Microservice.Models.Requests
{
    public class ContactFormSendRequest
    {
        [Required]
        public string ApplicationName { get; set; }
        [Required]
        public EFormType FormType { get; set; }
        public string UserId { get; set; }
        public string Subject { get; set; }
        public string FullName { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Message { get; set; }
    }
}
