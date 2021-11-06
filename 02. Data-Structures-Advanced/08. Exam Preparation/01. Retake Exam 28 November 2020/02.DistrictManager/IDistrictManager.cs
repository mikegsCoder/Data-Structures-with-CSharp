using System.Collections.Generic;

namespace _02.DistrictManager
{
    public interface IDistrictManager 
    {
        void CreateDistrict(District district, Country country);

        void CreateCountry(Country country);

        bool Contains(District district);

        bool Contains(Country country);

        Country RemoveCountry(int id);

        District RemoveDistrict(int id);

        int CountCountries();

        int CountDistricts();

        IEnumerable<District> GetDistricts(Country country);

        IEnumerable<District> GetDistrictsOrderedBySize();

        IEnumerable<Country> GetCountriesOrderedByPopulationThenByNameDesc();

        Dictionary<Country, HashSet<District>> GetCountriesAndDistrictsOrderedByDistrictsCountDescThenByCountryPopulationAsc();
    }
}