using System;
using System.Collections.Generic;
using System.Linq;

namespace Exam.MobileX
{
    public class VehicleRepository : IVehicleRepository
    {
        private Dictionary<string, Vehicle> byId;
        private Dictionary<string, HashSet<Vehicle>> bySeller;
        private Dictionary<string, HashSet<Vehicle>> byBrand;

        public VehicleRepository()
        {
            this.byId = new Dictionary<string, Vehicle>();
            this.bySeller = new Dictionary<string, HashSet<Vehicle>>();
            this.byBrand = new Dictionary<string, HashSet<Vehicle>>();
        }

        public int Count => this.byId.Count;

        public void AddVehicleForSale(Vehicle vehicle, string sellerName)
        {
            if (this.Contains(vehicle))
            {
                return;
            }

            vehicle.Seller = sellerName;

            this.byId.Add(vehicle.Id, vehicle);

            if (!this.bySeller.ContainsKey(sellerName))
            {
                this.bySeller.Add(sellerName, new HashSet<Vehicle>());
            }

            this.bySeller[sellerName].Add(vehicle);

            if (!this.byBrand.ContainsKey(vehicle.Brand))
            {
                this.byBrand.Add(vehicle.Brand, new HashSet<Vehicle>());
            }

            this.byBrand[vehicle.Brand].Add(vehicle);
        }

        public Vehicle BuyCheapestFromSeller(string sellerName)
        {
            if (!this.bySeller.ContainsKey(sellerName) || this.bySeller[sellerName].Count == 0)
            {
                throw new ArgumentException();
            }

            var cheapest = this.bySeller[sellerName].OrderBy(x => x.Price).First();

            if (cheapest == null)
            {
                throw new ArgumentException();

            }

            this.RemoveVehicle(cheapest.Id);

            return cheapest;
        }

        public bool Contains(Vehicle vehicle)
        {
            return this.byId.ContainsKey(vehicle.Id);
        }

        public Dictionary<string, List<Vehicle>> GetAllVehiclesGroupedByBrand()
        {
            if (this.Count == 0)
            {
                throw new ArgumentException();
            }

            var result = new Dictionary<string, List<Vehicle>>();

            foreach (var brand in this.byBrand.Keys.Where(x => this.byBrand[x].Count >= 0))
            {
                result.Add(brand, new List<Vehicle>());
                result[brand].AddRange(this.byBrand[brand].OrderBy(x => x.Price));                
            }

            return result;
        }

        public IEnumerable<Vehicle> GetAllVehiclesOrderedByHorsepowerDescendingThenByPriceThenBySellerName()
        {
            
            if (this.Count == 0)
            {
                return Enumerable.Empty<Vehicle>();
            }

            return this.byId.Values.OrderByDescending(x => x.Horsepower)
                .ThenBy(x => x.Price)
                .ThenBy(x => x.Seller);
        }

        public IEnumerable<Vehicle> GetVehicles(List<string> keywords)
        {
            return this.byId.Values
                .Where(x => (keywords.Contains(x.Brand)
                    || keywords.Contains(x.Model)
                    || keywords.Contains(x.Location)
                    || keywords.Contains(x.Color)))
                .OrderByDescending(x => x.IsVIP)
                .ThenBy(x => x.Price);
        }

        public IEnumerable<Vehicle> GetVehiclesBySeller(string sellerName)
        {
            if (!this.bySeller.ContainsKey(sellerName) || this.bySeller[sellerName].Count == 0)
            {
                throw new ArgumentException();
            }

            return this.bySeller[sellerName];
        }

        public IEnumerable<Vehicle> GetVehiclesInPriceRange(double lowerBound, double upperBound)
        {
            return this.byId.Values
                .Where(x => x.Horsepower >= lowerBound && x.Horsepower <= upperBound)
                .OrderByDescending(x => x.Horsepower);
        }

        public void RemoveVehicle(string vehicleId)
        {
            if (!this.byId.ContainsKey(vehicleId))
            {
                throw new ArgumentException();
            }

            var toRemove = this.byId[vehicleId];

            this.byId.Remove(vehicleId);

            this.bySeller[toRemove.Seller].Remove(toRemove);

            this.byBrand[toRemove.Brand].Remove(toRemove);
        }
    }
}
