using Form.Microservice.Models.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Form.Microservice.Modules
{
    public interface IContactFormModule
    {
        Task<long> AddAsync(ContactFormSendRequest Request);
        Task<IEnumerable<Entities.Form>> GetAsync(byte LastCount);
        Task<IEnumerable<Entities.Form>> GetAsync(ContactFromGetRequest Request);
    }
}
