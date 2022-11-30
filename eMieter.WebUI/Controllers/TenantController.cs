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
    public class TenantController : Controller
    {
        private readonly Tenants _tenants;
        private readonly AppConfig _appConfig;
        public TenantController(Tenants tenants, AppConfig appConfig)
        {
            _tenants = tenants;
            _appConfig = appConfig;
        }

        public IActionResult Index()
        {
            return View(_tenants.GetListByOwnerId(_appConfig.OwnerId));
        }
        public IActionResult AddEdit(tblTenant tenant, string Screen)
        {
            //tenant.OwnerId = _appConfig.OwnerId;
            tenant.CreatedBy = _appConfig.UserId;
            if (ModelState.IsValid)
            {
                _tenants.AddUpdate(tenant);
                if (!string.IsNullOrEmpty(Screen) && Screen == "List")
                {
                    var lst = _tenants.GetListByOwnerId(_appConfig.OwnerId);
                    return PartialView("_TenantList", lst);
                }
                else
                {
                    var detail = _tenants.GetbyId(tenant.Id);
                    return PartialView("_TenantDetail", detail);
                }

            }
            else
            {
                return Json(new { message = Resource.AllRequiredFields });
            }
        }
        public IActionResult Detail(Guid Id)
        {
            return View(_tenants.GetbyId(Id));
        }
        public IActionResult GetbyId(Guid Id)
        {
            return Json(_tenants.GetbyId(Id));
        }
        public IActionResult Delete(Guid Id)
        {
            _tenants.Delete(Id);
            return Json("");
        }
    }
}
