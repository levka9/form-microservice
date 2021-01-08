using Form.Microservice.Entities.Context;
using Form.Microservice.Models.Requests;
using Form.Microservice.Modules;
using FluentEmail.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Form.Microservice.Controllers
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
        public async Task<IActionResult> SendTestEmail()
        {
            var email = await Email.From("levkir222@email.com")
                                   .To("levkir@gmail.com", "bob")
                                   .Subject("hows it going bob")
                                   .Body("yo bob, long time no see!")
                                .SendAsync();
            return Ok(email.MessageId);
        }

        [HttpGet]
        public async Task<IEnumerable<Form.Microservice.Entities.Form>> Get([FromQuery] ContactFromGetRequest Request)
        {
            return await contactFormModule.GetAsync(Request);
        }

        [HttpGet]
        public async Task<IEnumerable<Form.Microservice.Entities.Form>> GetLastN([FromQuery] byte LastCount)
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
