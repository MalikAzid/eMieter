using eMieter.Entities;
using eMieter.Services.BLL;
using eMieter.ViewModel;
using eMieter.WebUI.Helper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eMieter.WebUI.Controllers
{
    public class AccountController : BaseController
    {
        private readonly Users _users;
        private readonly AppConfig _appConfig;
        private readonly MultiLanguage _multiLanguage;

        public AccountController(Users users, AppConfig appConfig, MultiLanguage multiLanguage) :base(multiLanguage)
        {
            _users = users;
            _appConfig = appConfig;
            _multiLanguage = multiLanguage;

        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Register(UserVM userVM)
        {
            var user = new tblUser()
            {
                Id = Guid.NewGuid(),
                Email = userVM.Email,
                Password = userVM.Password,
                CreatedDate = DateTime.Now
            };
            tblOwner owner = new tblOwner()
            {
                Id = Guid.NewGuid(),
                Name = userVM.FirstName + " " + userVM.LastName,
            };
            owner.tblUser = new List<tblUser>();
            owner.tblUser.Add(user);
            _users.RegisterOwner(owner);
            TempData["success"] = "success";
            return RedirectToAction("Login", "Account");
        }
        [HttpGet]
        public IActionResult Login()
        {
            string cookie = HttpContext.Request.Cookies["_eMU"];
            if (!string.IsNullOrEmpty(cookie))
            {
                var _user = _users.GetUserbyId(Guid.Parse(cookie));
                if (_user != null)
                {
                    return RedirectToAction("Index", "House");
                }
            }
            return View();
        }
        [HttpPost]
        public IActionResult Login(UserVM userVM)
        {
            var user = _users.Login(userVM.Email, userVM.Password);
            if (user != null)
            {
                _appConfig.SetSession(user, userVM.StayLogin);
                return RedirectToAction("Index", "House");
            }
            else
            {
                TempData["faild"] = "faild";
            }
            return View(userVM);
        }
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            Response.Cookies.Delete("_eMU");
            return RedirectToAction("Login", "Account");
        }
        public ActionResult IsEmailExist(string email)
        {
            return Json(_users.IsEmailAlreadyExist(email));
        }
    }
}
