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
    public class RentalPropertyController : BaseController
    {
        private readonly RentalPropertys _rentalPropertys;
        private readonly Houses _houses;
        private readonly AppConfig _appConfig;
        private readonly MultiLanguage _multiLanguage;

        public RentalPropertyController(Houses houses,RentalPropertys rentalPropertys, AppConfig appConfig, MultiLanguage multiLanguage) : base(multiLanguage)
        {
            _rentalPropertys = rentalPropertys;
            _houses = houses;
            _appConfig = appConfig;
            _multiLanguage = multiLanguage;

        }
        public IActionResult Index(Guid Id)
        {
            var house = _houses.GetbyId(Id);
            if (house != null && house.OwnerId ==_appConfig.OwnerId)
            {
                ViewBag.House = house;
                return View(_rentalPropertys.GetListByHouseId(Id));
            }
            else
            {
                return RedirectToAction("Index", "House");
            }
        }
        public IActionResult AddEdit(tblRentalProperty rentalProperty, string Screen)
        {
            rentalProperty.CreatedBy = _appConfig.UserId;
            if (ModelState.IsValid)
            {
                _rentalPropertys.AddUpdate(rentalProperty);
                if (!string.IsNullOrEmpty(Screen) && Screen == "List")
                {
                    var lst = _rentalPropertys.GetListByHouseId(rentalProperty.HouseId);
                    return PartialView("_RentalPropertyList", lst);
                }
                else
                {
                    var detail = _rentalPropertys.GetbyId(rentalProperty.Id);
                    return PartialView("_RentalPropertyDetail", detail);
                }
                
            }
            else
            {
                return Json(new { message = Resource.AllRequiredFields });
            }
        }
        public IActionResult GetbyId(Guid Id)
        {
            return Json(_rentalPropertys.GetbyId(Id));
        }
        public IActionResult Delete(Guid Id,Guid houseId)
        {
            _rentalPropertys.Delete(Id);
            var lst = _rentalPropertys.GetListByHouseId(houseId);
            return PartialView("_RentalPropertyList", lst);
        }
    }
}
