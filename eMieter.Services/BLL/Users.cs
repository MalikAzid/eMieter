using eMieter.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace eMieter.Services.BLL
{
    public class Users
    {
        private readonly eMieterContext _eMieterContext;
        public Users(eMieterContext eMieterContext)
        {
            _eMieterContext = eMieterContext;
        }
        public bool AddUpdateOwnerUser(tblUser user)
        {
            var old = _eMieterContext.tblUser.Where(i => i.Id == user.Id).FirstOrDefault();
            if (old != null)
            {
                old.Email = user.Email;
                old.Password = user.Password;
            }
            else
            {
                user.Id = Guid.NewGuid();
                user.CreatedDate = DateTime.Now;
                _eMieterContext.tblUser.Add(user);
            }
            return _eMieterContext.SaveChanges() > 0;
        }
        public List<tblUser> GetOwnerUserList(Guid ownerId)
        {
            return _eMieterContext.tblUser.Where(i => i.OwnerId == ownerId).ToList();
        }
        public tblUser GetUserbyId(Guid Id)
        {
            return _eMieterContext.tblUser.Include(i => i.tblOwner).Where(i => i.Id == Id).FirstOrDefault();
        }
        public bool DeletUser(Guid Id)
        {
            var old = _eMieterContext.tblUser.Where(i => i.Id == Id).FirstOrDefault();
            if (old != null)
            {
                _eMieterContext.tblUser.Remove(old);
            }
            return _eMieterContext.SaveChanges() > 0;
        }
        public bool IsEmailAlreadyExist(string email)
        {
           email = email == null ? "" : email;
            return _eMieterContext.tblUser.Where(i => i.Email.ToLower() == email.ToLower()).Any();
        }
        public bool RegisterOwner(tblOwner owner)
        {
            _eMieterContext.tblOwner.Add(owner);
            return _eMieterContext.SaveChanges() > 0;
        }
        public tblUser Login(string email, string password)
        {
            return _eMieterContext.tblUser.Include(i => i.tblOwner).Where(i => i.Email == email && i.Password == password).FirstOrDefault();
        }
    }
}
