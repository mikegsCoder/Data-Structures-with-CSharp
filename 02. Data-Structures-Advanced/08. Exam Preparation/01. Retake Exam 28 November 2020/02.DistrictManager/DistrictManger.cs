namespace _02.DistrictManager
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class DistrictManager : IDistrictManager
    {
        Dictionary<int, Country> countries = new Dictionary<int, Country>();
        Dictionary<int, District> districts = new Dictionary<int, District>();

        public void CreateDistrict(District district, Country country)
        {
            if (districts.ContainsKey(district.Id) || !countries.ContainsKey(country.Id))
            {
                throw new ArgumentException();
            }

            district.Country = country;
            districts.Add(district.Id, district);

            country.Districts.Add(district);
            countries[country.Id] = country;
        }

        public void CreateCountry(Country country)
        {
            if (countries.ContainsKey(country.Id))
            {
                throw new ArgumentException();
            }

            countries.Add(country.Id, country);
        }

        public bool Contains(District district)
        {
            return districts.ContainsKey(district.Id);
        }

        public bool Contains(Country country)
        {
            return countries.ContainsKey(country.Id);
        }

        public Country RemoveCountry(int id)
        {
            if (!countries.ContainsKey(id))
            {
                throw new ArgumentException();
            }

            var countryToRemove = countries[id];
            var districtsToRemove = countryToRemove.Districts;

            countries.Remove(id);

            foreach (var key in districtsToRemove.Select(d => d.Id))
            {
                districts.Remove(key);
            }

            return countryToRemove;
        }

        public District RemoveDistrict(int id)
        {
            if (!districts.ContainsKey(id))
            {
                throw new ArgumentException();
            }

            var districtToRemove = districts[id];
            var countryToUpdate = districtToRemove.Country;
            countryToUpdate.Districts.Remove(districtToRemove);
            countries[countryToUpdate.Id] = countryToUpdate;

            districts.Remove(id);

            return districtToRemove;
        }

        public int CountCountries()
        {
            return countries.Count;
        }

        public int CountDistricts()
        {
            return districts.Count;
        }

        public IEnumerable<District> GetDistricts(Country country)
        {
            if (!countries.ContainsKey(country.Id))
            {
                throw new ArgumentException();
            }

            if (countries[country.Id].Districts.Count == 0)
            {
                return new List<District>();
            }

            return countries[country.Id].Districts.ToList();
        }

        public IEnumerable<District> GetDistrictsOrderedBySize()
        {
            if (districts.Count == 0)
            {
                return new List<District>();
            }

            return districts.Values.OrderBy(d => d.SqMeters).ToList();
        }

        public IEnumerable<Country> GetCountriesOrderedByPopulationThenByNameDesc()
        {
            return countries.Values.OrderBy(c => c.Population).ThenByDescending(c => c.Name).ToList();
        }

        public Dictionary<Country, HashSet<District>> GetCountriesAndDistrictsOrderedByDistrictsCountDescThenByCountryPopulationAsc()
        {
            Dictionary<Country, HashSet<District>> dictionary = new Dictionary<Country, HashSet<District>>();

            IEnumerable<Country> query = countries.Values
                .OrderByDescending(c => c.Districts.Count)
                .ThenBy(c => c.Population)
                .ToArray();

            foreach (Country country in query)
            {
                dictionary[country] = country.Districts;
            }

            return dictionary;
        }
    }
}