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
    public class RentalPropertyController : Controller
    {
        private readonly RentalPropertys _rentalPropertys;
        private readonly Houses _houses;
        private readonly AppConfig _appConfig;
        public RentalPropertyController(Houses houses,RentalPropertys rentalPropertys, AppConfig appConfig)
        {
            _rentalPropertys = rentalPropertys;
            _houses = houses;
            _appConfig = appConfig;
        }
        public IActionResult Index(Guid Id)
        {
            ViewBag.House = _houses.GetbyId(Id);
            return View(_rentalPropertys.GetListByHouseId(Id));
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
