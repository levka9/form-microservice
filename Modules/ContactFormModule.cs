using Form.Microservice.Entities.Context;
using Form.Microservice.Models.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Form.Microservice.Modules
{
    public class ContactFormModule : IContactFormModule
    {
        #region Properties
        static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        FormContext context;
        #endregion

        public ContactFormModule(FormContext Context)
        {
            context = Context;
        }

        public async Task<IEnumerable<Entities.Form>> GetAsync(byte LastCount)
        {
            var query = $"SELECT TOP {LastCount} * FROM Form ORDER BY CreatedDate DESC";

            var lstContactForm = await context.Set<Entities.Form>().FromSqlRaw(query)
                                                                   .Include(x => x.FormDetails)                                                                        
                                                                   .ToListAsync();

            return lstContactForm;
        }

        public async Task<IEnumerable<Entities.Form>> GetAsync(ContactFromGetRequest Request)
        {
            var contactFormQuery = context.Set<Entities.Form>().AsQueryable();

            var lstContactForm  = await contactFormQuery.Include(x => x.FormDetails)
                                                        .Where(x => x.CreatedDate >= Request.FromDate &&
                                                                   (Request.ToDate == null || x.CreatedDate <= Request.ToDate))
                                                        .ToListAsync();            
            return lstContactForm;
        }

        public async Task<long> AddAsync(ContactFormSendRequest Request)
        {
            var contactForm = MapData(Request);

            context.Set<Entities.Form>().Add(contactForm);
            await context.SaveChangesAsync();

            return contactForm.Id;
        }

        private Entities.Form MapData(ContactFormSendRequest Request)
        {
            var contactForm = new Entities.Form();
            
            contactForm.ApplicationName = Request.ApplicationName;
            contactForm.FormTypeId = (byte)Request.FormType;
            contactForm.UserId = Request.UserId;

            contactForm.FormDetails = new List<Entities.FormDetails>();
            
            this.AddNewField(contactForm, nameof(Request.Subject), Request.Subject);
            this.AddNewField(contactForm, nameof(Request.Email), Request.Email);
            this.AddNewField(contactForm, nameof(Request.FullName), Request.FullName);
            this.AddNewField(contactForm, nameof(Request.Message), Request.Message);

            return contactForm;
        }

        private void AddNewField(Entities.Form ContactForm, string FieldName, string FieldValue)
        {
            var contactFormDetails = new Entities.FormDetails();
            contactFormDetails.FieldName = FieldName;
            contactFormDetails.FieldValue = FieldValue;

            ContactForm.FormDetails.Add(contactFormDetails);
        }
    }
}
