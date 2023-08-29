using EmployeeManage.Models;
using EmployeeManage.Repository.Interface;
using EmployeeManage.ViewModels.Responses;
using System.Collections.Generic;
using System.Linq;
namespace EmployeeManage.Repository
{
    public class RegionCountryCityRepo : IRegionCountryCity
    {
        private readonly EmployeesDBContext _db;

        public RegionCountryCityRepo(EmployeesDBContext db)
        {
            _db = db;
        }
        public IEnumerable<RegionCountryCityInfo> ListOfCityCountryRegion()
        {
            var totalRegions = _db.TblRegions.Count();

            return _db.TblRegions
                 .Join(_db.TblCountries,
                     region => region.RegionId,
                     country => country.RegionId,
                     (region, country) => new { region, country }
                    )
                 .GroupJoin(_db.TblCities,
                     data => data.country.CountryId,
                     city => city.CountryId,
                     (data, city) => new { data, city }
                     )
                .SelectMany(
                    obj => obj.city.DefaultIfEmpty(),
                    (result, city) => new RegionCountryCityInfo
                    {
                        RegionId = result.data.region.RegionId,
                        RegionName = result.data.region.RegionName,
                        CountryId = result.data.country.CountryId,
                        CountryName = result.data.country.CountryName,
                        CityId = city.CityId,
                        CityName = city.CityName,
                        RegionCount = totalRegions
                    })
                .ToList();
        }
    }
}