using ContactForm.Microservice.Models.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContactForm.Microservice.Modules
{
    public interface IContactFormModule
    {
        Task<long> AddAsync(ContactFormSendRequest Request);
        Task<IEnumerable<Entities.ContactForm>> GetAsync(byte LastCount);
        Task<IEnumerable<Entities.ContactForm>> GetAsync(ContactFromGetRequest Request);
    }
}
