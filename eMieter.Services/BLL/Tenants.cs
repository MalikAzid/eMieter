using eMieter.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace eMieter.Services.BLL
{
   public class Tenants
    {
        private readonly eMieterContext _eMieterContext;
        public Tenants(eMieterContext eMieterContext)
        {
            _eMieterContext = eMieterContext;
        }
        public bool AddUpdate(tblTenant tenant)
        {
            var old = _eMieterContext.tblTenant.Where(i => i.Id == tenant.Id).FirstOrDefault();
            if (old != null)
            {
                old.FirstName = tenant.FirstName;
                old.LastName = tenant.LastName;
                old.DateOfBirth = tenant.DateOfBirth;
                old.Rate = tenant.Rate;
            }
            else
            {
                tenant.Id = Guid.NewGuid();
                tenant.CreatedDate = DateTime.Now;
                _eMieterContext.tblTenant.Add(tenant);
            }
            return _eMieterContext.SaveChanges() > 0;
        }
        public List<tblTenant> GetListByOwnerId(Guid ownerId)
        {
            return _eMieterContext.tblTenant.ToList();
        }
        public tblTenant GetbyId(Guid Id)
        {
            return _eMieterContext.tblTenant.Where(i => i.Id == Id).FirstOrDefault();
        }
        public bool Delete(Guid Id)
        {
            var old = _eMieterContext.tblTenant.Where(i => i.Id == Id).FirstOrDefault();
            if (old != null)
            {
                _eMieterContext.tblTenant.Remove(old);
            }
            return _eMieterContext.SaveChanges() > 0;
        }
    }
}
