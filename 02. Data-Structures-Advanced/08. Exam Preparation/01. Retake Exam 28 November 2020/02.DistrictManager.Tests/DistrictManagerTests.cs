namespace _02.DistrictManager.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using NUnit.Framework;
    using _02.DistrictManager;

    public class DistrictManagerTests
    {
        private Country c1;
        private Country c2;
        private Country c3;
        private Country c4;

        private District d1;
        private District d2;
        private District d3;
        private District d4;
        private District d5;

        private IDistrictManager dm;

        [SetUp]
        public void Setup()
        {
            this.c1 = new Country(1, "country1", 100);
            this.c2 = new Country(2, "country2", 150);
            this.c3 = new Country(3, "country3", 50);
            this.c4 = new Country(4, "country12", 100);

            this.d1 = new District(1, "d1", 10);
            this.d2 = new District(2, "d2", 12);
            this.d3 = new District(3, "d3", 3);
            this.d4 = new District(4, "d4", 90);
            this.d5 = new District(5, "d5", 20);

            this.dm = new DistrictManager();
        }

        [Test]
        public void TestCreateCountry()
        {
            this.dm.CreateCountry(c1);

            Assert.True(this.dm.Contains(c1));
        }

        [Test]
        public void TestCreateCountryTwice()
        {
            this.dm.CreateCountry(c1);
            Assert.Throws<ArgumentException>(() => this.dm.CreateCountry(c1));
        }

        [Test]
        public void TestCreateMultipleCoyntires()
        {
            this.dm.CreateCountry(c1);
            this.dm.CreateCountry(c2);
            this.dm.CreateCountry(c3);

            Assert.AreEqual(3, this.dm.CountCountries());
            Assert.True(this.dm.Contains(c1));
            Assert.True(this.dm.Contains(c2));
            Assert.True(this.dm.Contains(c3));
        }

        [Test]
        public void TestCreateDistrictWithNoCountry()
        {
            Assert.Throws<ArgumentException>(() => this.dm.CreateDistrict(d1, c1));
        }

        [Test]
        public void TestCreateDistrict()
        {
            this.dm.CreateCountry(c1);
            this.dm.CreateDistrict(d1, c1);

            Assert.True(this.dm.Contains(c1));
        }

        [Test]
        public void TestCreateDistrictTwice()
        {
            this.dm.CreateCountry(c1);
            this.dm.CreateDistrict(d1, c1);
            Assert.Throws<ArgumentException>(() => this.dm.CreateDistrict(d1, c1));
        }

        [Test]
        public void TestCreateMultipleDistricts()
        {
            this.dm.CreateCountry(c1);
            this.dm.CreateDistrict(d1, c1);
            this.dm.CreateDistrict(d2, c1);
            this.dm.CreateDistrict(d3, c1);
            this.dm.CreateDistrict(d4, c1);

            Assert.AreEqual(4, this.dm.CountDistricts());
            Assert.True(this.dm.Contains(d1));
            Assert.True(this.dm.Contains(d2));
            Assert.True(this.dm.Contains(d3));
            Assert.True(this.dm.Contains(d4));
        }

        [Test]
        public void TestContainsCountryReturnTrue()
        {
            this.dm.CreateCountry(c1);
            Assert.True(this.dm.Contains(c1));
        }

        [Test]
        public void TestContainsCountryReturnFalse()
        {
            this.dm.CreateCountry(c1);
            Assert.False(this.dm.Contains(c2));
        }

        [Test]
        public void TestContainsDistrictReturnTrue()
        {
            this.dm.CreateCountry(c1);
            this.dm.CreateDistrict(d1, c1);

            Assert.True(this.dm.Contains(d1));
        }

        [Test]
        public void TestContainsDistrictReturnFalse()
        {
            this.dm.CreateCountry(c1);
            this.dm.CreateDistrict(d1, c1);

            Assert.False(this.dm.Contains(d2));
        }

        [Test]
        public void TestRemoveCountry()
        {
            this.dm.CreateCountry(c1);
            Assert.True(this.dm.Contains(c1));
            this.dm.RemoveCountry(c1.Id);
            Assert.False(this.dm.Contains(c1));
        }

        [Test]
        public void TestRemoveCountryThrowException()
        {
            Assert.Throws<ArgumentException>(() => this.dm.RemoveCountry(c1.Id));
        }

        [Test]
        public void TestRemoveDistrict()
        {
            this.dm.CreateCountry(c1);
            this.dm.CreateDistrict(d1, c1);
            Assert.True(this.dm.Contains(d1));
            this.dm.RemoveDistrict(d1.Id);
            Assert.False(this.dm.Contains(d1));
        }

        [Test]
        public void TestRemoveDistrictThrowException()
        {
            Assert.Throws<ArgumentException>(() => this.dm.RemoveDistrict(d1.Id));
        }

        [Test]
        public void TestRemoveCountryShouldRemoveItsDistricts()
        {
            this.dm.CreateCountry(c1);
            this.dm.CreateDistrict(d1, c1);
            this.dm.CreateDistrict(d2, c1);
            this.dm.CreateDistrict(d3, c1);

            this.dm.RemoveCountry(c1.Id);

            Assert.AreEqual(0, this.dm.CountCountries());
            Assert.AreEqual(0, this.dm.CountDistricts());
            Assert.False(this.dm.Contains(c1));
            Assert.False(this.dm.Contains(d1));
            Assert.False(this.dm.Contains(d2));
            Assert.False(this.dm.Contains(d3));
        }

        [Test]
        public void TestCountCountriesZero()
        {
            Assert.AreEqual(0, this.dm.CountCountries());
        }

        [Test]
        public void TestCountDistricts()
        {
            Assert.AreEqual(0, this.dm.CountDistricts());
        }

        [Test]
        public void TestCountCountryOne()
        {
            this.dm.CreateCountry(c1);

            Assert.AreEqual(1, this.dm.CountCountries());
        }

        [Test]
        public void TestCountDistrictsOne()
        {
            this.dm.CreateCountry(c1);
            this.dm.CreateDistrict(d1, c1);
            Assert.AreEqual(1, this.dm.CountDistricts());
        }

        [Test]
        public void TestCountCountriesMany()
        {
            this.dm.CreateCountry(c1);
            this.dm.CreateCountry(c2);
            this.dm.CreateCountry(c3);
            Assert.AreEqual(3, this.dm.CountCountries());
        }

        [Test]
        public void TestDistrictCountMany()
        {
            this.dm.CreateCountry(c1);
            this.dm.CreateDistrict(d1, c1);
            this.dm.CreateDistrict(d2, c1);
            this.dm.CreateDistrict(d3, c1);
            this.dm.CreateDistrict(d4, c1);

            Assert.AreEqual(4, this.dm.CountDistricts());
        }

        [Test]
        public void TestGetDistrictsNo()
        {
            this.dm.CreateCountry(c1);

            var res = this.dm.GetDistricts(c1).ToList();
            Assert.IsTrue(res.Count == 0);
        }

        [Test]
        public void TestGetDistrictsOne()
        {
            this.dm.CreateCountry(c1);
            this.dm.CreateDistrict(d1, c1);

            var res = this.dm.GetDistricts(c1).ToList();
            Assert.IsTrue(res.Count == 1);
        }

        [Test]
        public void TestGetDistrictsMany()
        {
            this.dm.CreateCountry(c1);
            this.dm.CreateDistrict(d1, c1);
            this.dm.CreateDistrict(d2, c1);
            this.dm.CreateDistrict(d3, c1);
            this.dm.CreateDistrict(d4, c1);
            this.dm.CreateDistrict(d5, c1);

            var res = this.dm.GetDistricts(c1).ToList();
            Assert.True(res.Count == 5);
        }

        [Test]
        public void TestGetDistrictsOrderedBySize()
        {
            this.dm.CreateCountry(c1);
            this.dm.CreateDistrict(d1, c1);
            this.dm.CreateDistrict(d2, c1);
            this.dm.CreateDistrict(d3, c1);
            this.dm.CreateDistrict(d4, c1);
            this.dm.CreateDistrict(d5, c1);

            var exp = new List<District>() { d3, d1, d2, d5, d4 };
            var res = this.dm.GetDistrictsOrderedBySize();

            CollectionAssert.AreEqual(exp, res);
        }

        [Test]
        public void TestGetDistrictsOrderedBySizeNo()
        {
            var res = this.dm.GetDistrictsOrderedBySize().ToList();
            Assert.True(res.Count == 0);
        }

        [Test]
        public void TestGetCountriesOrderedByPopulationThenByNameDesc()
        {
            this.dm.CreateCountry(c1);
            this.dm.CreateCountry(c2);
            this.dm.CreateCountry(c3);
            this.dm.CreateCountry(c4);

            var res = this.dm.GetCountriesOrderedByPopulationThenByNameDesc();
            var exp = new List<Country>() { c3, c4, c1, c2 };
            CollectionAssert.AreEqual(res, exp);
        }

        [Test]
        public void TestGetCountriesOrderedByPopulationThenByNameDescNo()
        {
            Assert.True(this.dm.GetCountriesOrderedByPopulationThenByNameDesc().ToList().Count == 0);
        }

        [Test]
        public void TestGetCountriesAndDistrictsOrderedByDistrictsCountDescThenByCountryPopulationAsc()
        {
            this.dm.CreateCountry(c2);
            this.dm.CreateCountry(c1);
            this.dm.CreateCountry(c3);

            this.dm.CreateDistrict(d1, c1);
            this.dm.CreateDistrict(d2, c2);
            this.dm.CreateDistrict(d3, c3);
            this.dm.CreateDistrict(d4, c1);
            this.dm.CreateDistrict(d5, c2);

            var res = this.dm.GetCountriesAndDistrictsOrderedByDistrictsCountDescThenByCountryPopulationAsc();
            var exp = new Dictionary<Country, HashSet<District>>() {
                {c1, new HashSet<District>() { d1, d4 }},
                {c2, new HashSet<District>() { d2, d5 }},
                {c3, new HashSet<District>() { d3 }}
            };

            CollectionAssert.AreEqual(res, exp);
        }

        [Test]
        public void TestGetCountriesAndDistrictsOrderedByDistrictsCountDescThenByCountryPopulationAscZero()
        {
            var res = this.dm.GetCountriesAndDistrictsOrderedByDistrictsCountDescThenByCountryPopulationAsc();
            var exp = new Dictionary<Country, HashSet<District>>();

            CollectionAssert.AreEqual(res, exp);
        }
    }
}