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
    public class HouseController : Controller
    {
        private readonly Houses _houses;
        private readonly AppConfig _appConfig;
        public HouseController(Houses houses, AppConfig appConfig)
        {
            _houses = houses;
            _appConfig = appConfig;
        }
        public IActionResult Index()
        {
            return View(_houses.GetListByOwnerId(_appConfig.OwnerId));
        }
        public IActionResult AddEdit(tblHouse house,string Screen)
        {
            house.OwnerId = _appConfig.OwnerId;
            house.CreatedBy = _appConfig.UserId;
            if (ModelState.IsValid)
            {
                _houses.AddUpdate(house);
                if (!string.IsNullOrEmpty(Screen) && Screen == "List")
                {
                    var lst = _houses.GetListByOwnerId(_appConfig.OwnerId);
                    return PartialView("_HouseList", lst);
                }
                else
                {
                    var detail = _houses.GetbyId(house.Id);
                    return PartialView("_HouseDetail", detail);
                }
            }
            else
            {
                return Json(new { message = Resource.AllRequiredFields});
            }
        }
        public IActionResult GetbyId(Guid Id)
        {
            return Json(_houses.GetbyId(Id));
        }
        public IActionResult Delete(Guid Id)
        {
            _houses.Delete(Id);
            var lst = _houses.GetListByOwnerId(_appConfig.OwnerId);
            return PartialView("_HouseList", lst);
        }
    }
}
