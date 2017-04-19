﻿using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;

namespace PrinterTonerEPCwithAuthentication.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            
            // Add custom user claims here
            

            return userIdentity;
        }
        public string Nick { get; set; }
        public virtual ICollection<ToDo> ToDoes { get; set; }

    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public DbSet<Owner> Owners { get; set; }
        public DbSet<Printer> Printers { get; set; }
        public DbSet<Toner> Toners { get; set; }
        public DbSet<Sale> Sales { get; set; }
        public DbSet<Contract> Contracts { get; set; }
        public DbSet<Servis> Servis { get; set; }
        public DbSet<SaleToner> SaleToners { get; set; }
        public DbSet<PrinterTonerCompatibility> PrinterTonerCompatibilitys { get; set; }
        //public DbSet<User> Users { get; set; }
        public DbSet<ToDo> ToDoes { get; set; }

        internal void SubmitChanges()
        {
            throw new NotImplementedException();
        }

        //public System.Data.Entity.DbSet<PrinterTonerEPCwithAuthentication.Models.User> Users1 { get; set; }
    }
}