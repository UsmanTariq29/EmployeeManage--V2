using EmployeeManage.ViewModels.Responses;
using System.Collections.Generic;

namespace EmployeeManage.Repository.Interface
{
    public interface IRegionCountryCity
    {
        public IEnumerable<RegionCountryCityInfo> ListOfCityCountryRegion();
    }
}
