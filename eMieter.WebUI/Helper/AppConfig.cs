using eMieter.Entities;
using eMieter.Services.BLL;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eMieter.WebUI.Helper
{
    public class AppConfig
    {
        public readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IConfiguration _configuration;
        private readonly Users _users;
        public AppConfig(IHttpContextAccessor httpContextAccessor, IConfiguration configuration, Users users)
        {
            _httpContextAccessor = httpContextAccessor;
            _configuration = configuration;
            _users = users;
        }
        public Guid UserId
        {
            get
            {
                if (_httpContextAccessor.HttpContext.Session.GetString("UserId") == null)
                {
                    BindSessionAgain();
                }
                return _httpContextAccessor.HttpContext.Session.GetString("UserId") != null ? Guid.Parse(_httpContextAccessor.HttpContext.Session.GetString("UserId")) : Guid.Empty;
            }
        }
        public Guid OwnerId
        {
            get
            {
                if (_httpContextAccessor.HttpContext.Session.GetString("OwnerId") == null)
                {
                    BindSessionAgain();
                }
                return _httpContextAccessor.HttpContext.Session.GetString("OwnerId") != null ? Guid.Parse(_httpContextAccessor.HttpContext.Session.GetString("OwnerId")) : Guid.Empty;
            }
        }
        public string Name
        {
            get
            {
                if (_httpContextAccessor.HttpContext.Session.GetString("Name") == null)
                {
                    BindSessionAgain();
                }
                return _httpContextAccessor.HttpContext.Session.GetString("Name") != null ? _httpContextAccessor.HttpContext.Session.GetString("Name") : "";
            }
        }
        public string Email
        {
            get
            {
                if (_httpContextAccessor.HttpContext.Session.GetString("Email") == null)
                {
                    BindSessionAgain();
                }
                return _httpContextAccessor.HttpContext.Session.GetString("Email") != null ? _httpContextAccessor.HttpContext.Session.GetString("Email") : "";
            }
        }
        public void SetSession(tblUser user, bool stayLogin)
        {
            _httpContextAccessor.HttpContext.Session.SetString("UserId", user.Id.ToString());
            _httpContextAccessor.HttpContext.Session.SetString("Email", user.Email);
            if (user.tblOwner != null)
            {
                _httpContextAccessor.HttpContext.Session.SetString("OwnerId", user.OwnerId.ToString());
                _httpContextAccessor.HttpContext.Session.SetString("Name", user.tblOwner.Name);
            }
            if (stayLogin)
            {
                CookieOptions option = new CookieOptions();
                option.Expires = DateTime.Now.AddYears(1);
                _httpContextAccessor.HttpContext.Response.Cookies.Append("_eMU", user.Id.ToString(), option);
            }
        }
        public void BindSessionAgain()
        {
            string cookie = _httpContextAccessor.HttpContext.Request.Cookies["_eMU"];
            if (!string.IsNullOrEmpty(cookie))
            {
                var _user = _users.GetUserbyId(Guid.Parse(cookie));
                if (_user != null)
                {
                    SetSession(_user, false);
                }
            }
        }
    }
}
