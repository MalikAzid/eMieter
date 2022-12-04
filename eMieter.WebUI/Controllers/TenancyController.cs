using eMieter.Entities;
using eMieter.Services.BLL;
using eMieter.WebUI.Helper;
using eMieter.WebUI.Resources;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eMieter.WebUI.Controllers
{
    public class TenancyController : BaseController
    {
        private readonly RentalPropertys _rentalPropertys;
        private readonly Tenancys _tenancys;
        private readonly AppConfig _appConfig;
        private readonly MultiLanguage _multiLanguage;

        public TenancyController(Tenancys tenancys, RentalPropertys rentalPropertys, AppConfig appConfig, MultiLanguage multiLanguage) : base(multiLanguage)
        {
            _rentalPropertys = rentalPropertys;
            _tenancys = tenancys;
            _appConfig = appConfig;
            _multiLanguage = multiLanguage;
        }
        public IActionResult Index(Guid Id)
        {
            var tenancyDetail = _rentalPropertys.GetbyId(Id);
            var ownerId = _rentalPropertys.GetOwnerIdByRentalPropertyId(Id);
            if (tenancyDetail != null && ownerId == _appConfig.OwnerId)
            {
                ViewBag.RentalProperty = tenancyDetail;
                return View(_tenancys.GetListByRentalPropertyId(Id));
            }
            else
            {
                return RedirectToAction("Index", "House");
            }
        }
        public IActionResult AddEdit(tblTenancy tenancy, string Screen)
        {
            tenancy.CreatedBy = _appConfig.UserId;
            tenancy.tblTenant.CreatedBy = _appConfig.UserId;
            if (ModelState.IsValid)
            {
                if (tenancy.RentalEndDate.HasValue && tenancy.RentalStartDate > tenancy.RentalEndDate)
                {
                    return Json(new { message = Resource.EndDateGreaterThan });
                }
                var isAnyActive = _tenancys.IsActiveTenancy(tenancy.RentalPropertyId, tenancy.RentalStartDate, tenancy.RentalEndDate);
                if (isAnyActive)
                {
                    return Json(new { message = Resource.TenancyisActive });
                }
                _tenancys.AddUpdate(tenancy);
                if (!string.IsNullOrEmpty(Screen) && Screen == "List")
                {
                    var lst = _tenancys.GetListByRentalPropertyId(tenancy.RentalPropertyId);
                    return PartialView("_TenancyList", lst);
                }
                else
                {
                    var detail = _tenancys.GetbyId(tenancy.Id);
                    return PartialView("_TenancyDetail", detail);
                }
            }
            else
            {
                return Json(new { message = Resource.AllRequiredFields });
            }
        }
        public IActionResult Detail(Guid Id)
        {
            var detail = _tenancys.GetbyId(Id);
            var ownerId = _tenancys.GetOwnerIdByTenancyId(Id);
            if (detail != null && ownerId == _appConfig.UserId)
            {
                return View(detail);
            }
            else
            {
                return RedirectToAction("Index", "House");
            }
        }
        public IActionResult GetbyId(Guid Id)
        {
            return Json(_tenancys.GetbyId(Id));
        }
        public IActionResult Delete(Guid Id, Guid rentalPropertyId)
        {
            _tenancys.Delete(Id);
            return Json("");
        }
    }
}
