using EmployeeManage.Repository.Interface;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManage.Controllers
{
    public class RegionCountryCityController : Controller
    {
        private readonly IRegionCountryCity _regionCountryCity;

        public RegionCountryCityController(IRegionCountryCity regionCountryCity)
        {
            _regionCountryCity = regionCountryCity;
        }
        public IActionResult CountryRegionCity()
        {
            var regioncitycountry = _regionCountryCity.ListOfCityCountryRegion();
            return View(regioncitycountry);
        }
    }
}