using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using ContactForm.Microservice.Entities;

namespace ContactForm.Microservice.Entities.Context
{
    public partial class ContactFormContext : DbContext
    {
        public ContactFormContext()
        {
        }

        public ContactFormContext(DbContextOptions<ContactFormContext> options)
            : base(options)
        {
        }

        public virtual DbSet<ContactForm> ContactForm { get; set; }
        public virtual DbSet<ContactFormDetails> ContactFormDetails { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=D:\\DotNet\\ContactForm.Microservice\\LocalDB\\ContactForm.mdf;Integrated Security=True;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ContactForm>(entity =>
            {
                entity.Property(e => e.ApplicationName)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.ContactFormType)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .IsFixedLength()
                    .HasComment("ContactForm, SupportForm");

                entity.Property(e => e.CreatedDate).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.UserId)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .IsFixedLength();
            });

            modelBuilder.Entity<ContactFormDetails>(entity =>
            {
                entity.Property(e => e.CreateDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Email)
                    .HasMaxLength(30)
                    .IsFixedLength();

                entity.Property(e => e.FullName)
                    .HasMaxLength(30)
                    .IsFixedLength();

                entity.Property(e => e.Message).IsRequired();

                entity.Property(e => e.Subjeсt)
                    .HasMaxLength(30)
                    .IsFixedLength();

                entity.HasOne(d => d.ContactForm)
                    .WithMany(p => p.ContactFormDetails)
                    .HasForeignKey(d => d.ContactFormId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ContactFormDetails_ToContactForm");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
