using NUnit.Framework;
using System.Collections.Generic;

namespace Exam.MobileX.Tests
{
    public class VehicleRepositoryTests
    {
        private IVehicleRepository vehicleRepository;

        [SetUp]
        public void Setup()
        {
            this.vehicleRepository = new VehicleRepository();
        }

        [Test]
        [Category("Correctness")]
        public void TestAddVehicle_WithCorrectData_ShouldCorrectlyAddVehicle()
        {
            Vehicle vehicle = new Vehicle(1 + "", "BMW", "X5", "Sofia", "Blue", 400, 50000, true);

            this.vehicleRepository.AddVehicleForSale(vehicle, "George");

            Assert.IsTrue(this.vehicleRepository.Contains(vehicle));
        }

        [Test]
        [Category("Correctness")]
        public void TestAddVehicle_WithCorrectData_ShouldCorrectlyIncrementCount()
        {
            Vehicle vehicle = new Vehicle(1 + "", "BMW", "X5", "Sofia", "Blue", 400, 50000, true);

            this.vehicleRepository.AddVehicleForSale(vehicle, "George");

            Assert.AreEqual(1, this.vehicleRepository.Count);
        }

        [Test]
        [Category("Correctness")]
        public void TestContains_WithExistentVehicle_ShouldReturnTrue()
        {
            Vehicle vehicle = new Vehicle(1 + "", "BMW", "X5", "Sofia", "Blue", 400, 50000, true);

            this.vehicleRepository.AddVehicleForSale(vehicle, "George");

            Assert.IsTrue(this.vehicleRepository.Contains(vehicle));
        }

        [Category("Correctness")]
        public void TestGetVehicles_WithExistentVehicles_ShouldCorrectlyOrderedVehicles()
        {
            Vehicle vehicle = new Vehicle(1 + "", "BMW", "X5", "Sofia", "Blue", 400, 90000, true);
            Vehicle vehicle2 = new Vehicle(2 + "", "Ford", "Escort", "Plovdiv", "Magenta", 500, 61000, false);
            Vehicle vehicle3 = new Vehicle(3 + "", "Audi", "A3", "Ruse", "Red", 300, 70000, false);
            Vehicle vehicle4 = new Vehicle(4 + "", "Audi", "A3", "Ruse", "Green", 500, 88000, true);
            Vehicle vehicle5 = new Vehicle(5 + "", "Audi", "A3", "Varna", "Magenta", 500, 50000, false);
            Vehicle vehicle6 = new Vehicle(6 + "", "Porsche", "Cayenne", "Plovdiv", "Black", 600, 55000, true);

            Vehicle vehicle7 = new Vehicle(7 + "", "Mercedes-Benz", "C220", "Plovdiv", "Black", 600, 100000, true);
            Vehicle vehicle8 = new Vehicle(8 + "", "Ford", "Mustang", "Plovdiv", "Black", 600, 110000, true);

            this.vehicleRepository.AddVehicleForSale(vehicle, "George");
            this.vehicleRepository.AddVehicleForSale(vehicle2, "Jack");
            this.vehicleRepository.AddVehicleForSale(vehicle3, "Phill");
            this.vehicleRepository.AddVehicleForSale(vehicle4, "Isacc");
            this.vehicleRepository.AddVehicleForSale(vehicle5, "Igor");
            this.vehicleRepository.AddVehicleForSale(vehicle6, "Donald");
            this.vehicleRepository.AddVehicleForSale(vehicle7, "John");
            this.vehicleRepository.AddVehicleForSale(vehicle8, "Jerry");

            List<Vehicle> vehicles = new List<Vehicle>(this.vehicleRepository.GetVehicles(new List<string>(new string[] { "BMW", "Cayenne", "Ruse", "Magenta" })));

            Assert.AreEqual(6, vehicles.Count);

            Assert.AreEqual(vehicle6, vehicles[0]);
            Assert.AreEqual(vehicle4, vehicles[1]);
            Assert.AreEqual(vehicle, vehicles[2]);
            Assert.AreEqual(vehicle5, vehicles[3]);
            Assert.AreEqual(vehicle2, vehicles[4]);
            Assert.AreEqual(vehicle3, vehicles[5]);
        }
    }
}