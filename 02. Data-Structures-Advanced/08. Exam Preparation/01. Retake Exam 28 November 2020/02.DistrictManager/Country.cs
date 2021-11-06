using System.Collections.Generic;

namespace _02.DistrictManager
{
    public class Country 
    {
        public Country(int id, string name, int population) 
        {
            this.Id = id;
            this.Name = name;
            this.Population = population;
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public int Population { get; set; }

        public HashSet<District> Districts { get; set; } = new HashSet<District>();
    }
}