
namespace EmployeeManage.ViewModels.Responses
{
    public class RegionCountryCityInfo
    {
        public int RegionId { get; set; }
        public string RegionName { get; set; }
        public int CountryId { get; set; }
        public string CountryName { get; set; }
        public int? CityId { get; set; }
        public string CityName { get; set; }
        public int RegionCount { get; set; }
    }
}