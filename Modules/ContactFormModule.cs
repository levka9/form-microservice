using ContactForm.Microservice.Entities.Context;
using ContactForm.Microservice.Models.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ContactForm.Microservice.Modules
{
    public class ContactFormModule : IContactFormModule
    {
        #region Properties
        static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        ContactFormContext context;
        #endregion

        public ContactFormModule(ContactFormContext Context)
        {
            context = Context;
        }

        public async Task<IEnumerable<Entities.ContactForm>> GetAsync(byte LastCount)
        {
            var contactFormQuery = context.Set<Entities.ContactForm>().AsQueryable();

            var lstContactForm = await contactFormQuery.TakeLast(LastCount)
                                                       .ToListAsync();

            return lstContactForm;
        }

        public async Task<IEnumerable<Entities.ContactForm>> GetAsync(ContactFromGetRequest Request)
        {
            var contactFormQuery = context.Set<Entities.ContactForm>().AsQueryable();

            var lstContactForm  = await contactFormQuery.Include(x => x.ContactFormDetails)
                                                        .Where(x => x.CreatedDate >= Request.FromDate &&
                                                                   (Request.ToDate == null || x.CreatedDate <= Request.ToDate))
                                                        .ToListAsync();            
            return lstContactForm;
        }

        public async Task<long> AddAsync(ContactFormSendRequest Request)
        {
            var contactForm = MapData(Request);

            context.Set<Entities.ContactForm>().Add(contactForm);
            await context.SaveChangesAsync();

            return contactForm.Id;
        }

        private Entities.ContactForm MapData(ContactFormSendRequest Request)
        {
            var contactForm = new Entities.ContactForm();
            var contactFormDetails = new Entities.ContactFormDetails();
            contactForm.ApplicationName = Request.ApplicationName;
            contactForm.ContactFormType = Request.ContactFormType;
            contactForm.ContactFormType = Request.ContactFormType;
            contactForm.UserId = Request.UserId;
            contactFormDetails.Email = Request.Email;
            contactFormDetails.FullName = Request.FullName;
            contactFormDetails.Message = Request.Message;
            contactFormDetails.Subjeсt = Request.Subject;

            contactForm.ContactFormDetails = new List<Entities.ContactFormDetails>();
            contactForm.ContactFormDetails.Add(contactFormDetails);

            return contactForm;
        }
    }
}
