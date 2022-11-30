using eMieter.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace eMieter.Services
{
   public class eMieterContext:DbContext
    {
        public eMieterContext(DbContextOptions<eMieterContext> options) : base(options) 
        {

        }
        public virtual DbSet<tblUser> tblUser { get; set; }
        public virtual DbSet<tblOwner> tblOwner { get; set; }
        public virtual DbSet<tblHouse> tblHouse { get; set; }
        public virtual DbSet<tblRentalProperty> tblRentalProperty { get; set; }
        public virtual DbSet<tblTenancy> tblTenancy { get; set; }
        public virtual DbSet<tblTenant> tblTenant { get; set; }
    }
}
