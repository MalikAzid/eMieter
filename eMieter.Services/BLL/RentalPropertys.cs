using eMieter.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace eMieter.Services.BLL
{
   public class RentalPropertys
    {
        private readonly eMieterContext _eMieterContext;
        public RentalPropertys(eMieterContext eMieterContext)
        {
            _eMieterContext = eMieterContext;
        }
        public bool AddUpdate(tblRentalProperty rentalProperty)
        {
            var old = _eMieterContext.tblRentalProperty.Where(i => i.Id == rentalProperty.Id).FirstOrDefault();
            if (old != null)
            {
                old.EWID = rentalProperty.EWID;
                old.LivingAreaSquareMeters = rentalProperty.LivingAreaSquareMeters;
                old.RoomCount = rentalProperty.RoomCount;
                old.Floor = rentalProperty.Floor;
                old.Location = rentalProperty.Location;
            }
            else
            {
                rentalProperty.Id = Guid.NewGuid();
                rentalProperty.CreatedDate = DateTime.Now;
                _eMieterContext.tblRentalProperty.Add(rentalProperty);
            }
            return _eMieterContext.SaveChanges() > 0;
        }
        public List<tblRentalProperty> GetListByHouseId(Guid houseId)
        {
            return _eMieterContext.tblRentalProperty.Include(i=> i.tblTenancy).ThenInclude(i=> i.tblTenant).Where(i => i.HouseId == houseId).ToList();
        }
        public tblRentalProperty GetbyId(Guid Id)
        {
            return _eMieterContext.tblRentalProperty.Where(i => i.Id == Id).FirstOrDefault();
        }
        public bool Delete(Guid Id)
        {
            var old = _eMieterContext.tblRentalProperty.Where(i => i.Id == Id).FirstOrDefault();
            if (old != null)
            {
                _eMieterContext.tblRentalProperty.Remove(old);
            }
            return _eMieterContext.SaveChanges() > 0;
        }
        public Guid GetOwnerIdByRentalPropertyId(Guid rentalPropertyId)
        {
            return _eMieterContext.tblRentalProperty.Include(i => i.tblHouse).Where(i => i.Id == rentalPropertyId).Select(i=> i.tblHouse.OwnerId).FirstOrDefault();
        }
    }
}
