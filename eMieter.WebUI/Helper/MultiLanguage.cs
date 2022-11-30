using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace eMieter.WebUI.Helper
{
    public class MultiLanguage
    {
        public readonly IHttpContextAccessor _httpContextAccessor;
        public MultiLanguage(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public List<Languages> AvailableLanguages()
        {
            List<Languages> lstLanguage = new List<Languages>();
            lstLanguage.Add(new Languages() { LanguageFullName = "English", LanguageCultureName = "en" });
            lstLanguage.Add(new Languages() { LanguageFullName = "German", LanguageCultureName = "de" });
            return lstLanguage;
        }
        public bool IsLanguageAvailable(string lang)
        {
            return AvailableLanguages().Where(a => a.LanguageCultureName.Equals(lang)).FirstOrDefault() != null ? true : false;
        }
        public string GetDefaultLanguage()
        {
            return AvailableLanguages()[0].LanguageCultureName;
        }
        public void SetLanguage(string lang)
        {
            try
            {
                if (!IsLanguageAvailable(lang)) lang = GetDefaultLanguage();
                var cultureInfo = new CultureInfo(lang);
                Thread.CurrentThread.CurrentUICulture = cultureInfo;
                Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(cultureInfo.Name);
                CookieOptions option = new CookieOptions();
                option.Expires = DateTime.Now.AddYears(1);
                _httpContextAccessor.HttpContext.Response.Cookies.Append("culture", lang, option);
            }
            catch (Exception) { }
        }
    }
    public class Languages
    {
        public string LanguageFullName { get; set; }
        public string LanguageCultureName { get; set; }
    }
}
