using eMieter.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace eMieter.Services.BLL
{
    public class Tenancys
    {
        private readonly eMieterContext _eMieterContext;
        public Tenancys(eMieterContext eMieterContext)
        {
            _eMieterContext = eMieterContext;
        }
        public bool AddUpdate(tblTenancy tenancy)
        {
            var old = _eMieterContext.tblTenancy.Include(i => i.tblTenant).Where(i => i.Id == tenancy.Id).FirstOrDefault();
            if (old != null)
            {
                old.RentalAdditionalCostsAkonto = tenancy.RentalAdditionalCostsAkonto;
                old.RentalCostNetMonthly = tenancy.RentalCostNetMonthly;
                old.RentalEndDate = tenancy.RentalEndDate;
                old.RentalStartDate = tenancy.RentalStartDate;
                old.TenantId = tenancy.TenantId;
                old.tblTenant.FirstName = tenancy.tblTenant.FirstName;
                old.tblTenant.LastName = tenancy.tblTenant.LastName;
                old.tblTenant.Rate = tenancy.tblTenant.Rate;
                old.tblTenant.DateOfBirth = tenancy.tblTenant.DateOfBirth;
            }
            else
            {
                tenancy.Id = Guid.NewGuid();
                tenancy.CreatedDate = DateTime.Now;
                tenancy.tblTenant.Id = Guid.NewGuid();
                tenancy.tblTenant.CreatedDate = DateTime.Now;
                _eMieterContext.tblTenancy.Add(tenancy);
            }
            return _eMieterContext.SaveChanges() > 0;
        }
        public List<tblTenancy> GetListByRentalPropertyId(Guid rentalPropertyId)
        {
            return _eMieterContext.tblTenancy.Include(i => i.tblTenant).Where(i => i.RentalPropertyId == rentalPropertyId).OrderByDescending(i => i.RentalStartDate).ToList();
        }
        public bool IsActiveTenancy(Guid rentalPropertyId, DateTime startDate, DateTime? endDate)
        {
            if ((_eMieterContext.tblTenancy.Any(t => t.RentalEndDate == null)) || (endDate == null && _eMieterContext.tblTenancy.Any(t => t.RentalEndDate > startDate)) || (_eMieterContext.tblTenancy.Any(t => t.RentalStartDate < endDate && startDate < t.RentalEndDate)))
            {
                return true;
            }
            return false;
        }
        public tblTenancy GetbyId(Guid Id)
        {
            return _eMieterContext.tblTenancy.Include(i => i.tblTenant).Where(i => i.Id == Id).FirstOrDefault();
        }
        public bool Delete(Guid Id)
        {
            var old = _eMieterContext.tblTenancy.Where(i => i.Id == Id).FirstOrDefault();
            if (old != null)
            {
                _eMieterContext.tblTenancy.Remove(old);
            }
            return _eMieterContext.SaveChanges() > 0;
        }
    }
}
