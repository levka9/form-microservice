using ContactForm.Microservice.Entities.Context;
using ContactForm.Microservice.Models.Requests;
using ContactForm.Microservice.Modules;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContactForm.Microservice.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class ContactFromController : ControllerBase
    {
        IContactFormModule contactFormModule;

        public ContactFromController(IContactFormModule ContactFormModule)
        {
            contactFormModule = ContactFormModule;
        }

        [HttpGet]
        public async Task<IEnumerable<Entities.ContactForm>> Get([FromQuery] ContactFromGetRequest Request)
        {
            return await contactFormModule.GetAsync(Request);
        }

        [HttpGet]
        public async Task<IEnumerable<Entities.ContactForm>> GetLastN([FromQuery] byte LastCount)
        {
            return await contactFormModule.GetAsync(LastCount);
        }

        [HttpPost]
        public async Task<IActionResult> Send([FromBody]ContactFormSendRequest Request)
        {
            var contactFormId = await contactFormModule.AddAsync(Request);

            return Ok(new { contactFormId = contactFormId });
        }
    }
}
