using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eMieter.Entities;
using eMieter.Services.BLL;
using eMieter.WebUI.Helper;
using eMieter.WebUI.Resources;
using Microsoft.AspNetCore.Mvc;

namespace eMieter.WebUI.Controllers
{
    public class UserController : BaseController
    {
        private readonly Users _users;
        private readonly AppConfig _appConfig;
        private readonly MultiLanguage _multiLanguage;

        public UserController(Users users, AppConfig appConfig, MultiLanguage multiLanguage):base(multiLanguage)
        {
            _users = users;
            _appConfig = appConfig;
            _multiLanguage = multiLanguage;
        }

        public IActionResult Index()
        {
            return View(_users.GetOwnerUserList(_appConfig.OwnerId));
        }
        public IActionResult AddEdit(tblUser user, string Screen)
        {
            user.OwnerId = _appConfig.OwnerId;
            user.CreatedBy = _appConfig.UserId;
            if (ModelState.IsValid)
            {
                _users.AddUpdateOwnerUser(user);
                if (!string.IsNullOrEmpty(Screen) && Screen == "List")
                {
                    var lst = _users.GetOwnerUserList(_appConfig.OwnerId);
                    return PartialView("_UserList", lst);
                }
                else
                {
                    var detail = _users.GetUserbyId(user.Id);
                    return PartialView("_UserDetail", detail);
                }

            }
            else
            {
                return Json(new { message = Resource.AllRequiredFields });
            }
        }
        public IActionResult Detail(Guid Id)
        {
            return View(_users.GetUserbyId(Id));
        }
        public IActionResult GetbyId(Guid Id)
        {
            return Json(_users.GetUserbyId(Id));
        }
        public IActionResult Delete(Guid Id)
        {
            _users.DeletUser(Id);
            var lst = _users.GetOwnerUserList(_appConfig.OwnerId);
            return PartialView("_UserList", lst);
        }
        public IActionResult ChangeLanguage(string lang)
        {
            _multiLanguage.SetLanguage(lang);
            return Json("");
        }
    }
}
