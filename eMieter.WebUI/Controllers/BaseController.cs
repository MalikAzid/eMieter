using eMieter.WebUI.Helper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eMieter.WebUI.Controllers
{
    public class BaseController : Controller
    {
        private readonly MultiLanguage _multiLanguage;
        public  IActionResult BeginExecuteCoreActionResult { get; set; }
        public BaseController( MultiLanguage multiLanguage)
        {
            _multiLanguage = multiLanguage;
        }
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            string lang = null;

            var langCookie = Request.Cookies["culture"];
            if (langCookie != null)
            {
                lang = "en";//langCookie;
            }
            else
            {
                //var userLanguage = "";// Request.UserLanguages;
                //var userLang = userLanguage != null ? userLanguage[0] : "";
                //if (userLang != "")
                //{
                //    lang = userLang;
                //}
                //else
                //{
                //}
                lang = _multiLanguage.GetDefaultLanguage();
            }
            _multiLanguage.SetLanguage(lang);
            //filterContext.Result = RedirectToAction("Index", "Home");// this.BeginExecuteCoreActionResult;
            //base.OnActionExecuting(filterContext);
        }
        //protected override IAsyncResult BeginExecuteCore(AsyncCallback callback, object state)
        //{
        //    string lang = null;

        //    var langCookie = Request.Cookies["culture"];
        //    if (langCookie != null)
        //    {
        //        lang = langCookie;
        //    }
        //    else
        //    {
        //        //var userLanguage = "";// Request.UserLanguages;
        //        //var userLang = userLanguage != null ? userLanguage[0] : "";
        //        //if (userLang != "")
        //        //{
        //        //    lang = userLang;
        //        //}
        //        //else
        //        //{
        //        //}
        //            lang = _multiLanguage.GetDefaultLanguage();
        //    }
        //    _multiLanguage.SetLanguage(lang);
        //    return base.OnActionExecuting();
        //}
    }
}
