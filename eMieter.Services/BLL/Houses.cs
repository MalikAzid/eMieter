using eMieter.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace eMieter.Services.BLL
{
   public class Houses
    {
        private readonly eMieterContext _eMieterContext;
        public Houses(eMieterContext eMieterContext)
        {
            _eMieterContext = eMieterContext;
        }
        public bool AddUpdate(tblHouse user)
        {
            var old = _eMieterContext.tblHouse.Where(i => i.Id == user.Id).FirstOrDefault();
            if (old != null)
            {
                old.Category = user.Category;
                old.Street = user.Street;
                old.PostCode = user.PostCode;
                old.City = user.City;
            }
            else
            {
                user.Id = Guid.NewGuid();
                user.CreatedDate = DateTime.Now;
                _eMieterContext.tblHouse.Add(user);
            }
            return _eMieterContext.SaveChanges() > 0;
        }
        public List<tblHouse> GetListByOwnerId(Guid ownerId)
        {
            return _eMieterContext.tblHouse.Where(i => i.OwnerId == ownerId).ToList();
        }
        public tblHouse GetbyId(Guid Id)
        {
            return _eMieterContext.tblHouse.Where(i => i.Id == Id).FirstOrDefault();
        }
        public bool Delete(Guid Id)
        {
            var old = _eMieterContext.tblHouse.Where(i => i.Id == Id).FirstOrDefault();
            if (old != null)
            {
                _eMieterContext.tblHouse.Remove(old);
            }
            return _eMieterContext.SaveChanges() > 0;
        }
    }
}
