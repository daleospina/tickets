using cicloso.tickets.core.Bussiness;
using cicloso.tickets.core.Data.Interfaces;
using cicloso.tickets.entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cicloso.tickets.tests
{
    [TestClass]
    public class CityTest
    {
        private Mock<ICityData> cityDataMock = null;
        private CityBussiness cityBussiness = null;

        [TestInitialize]
        public void InitializeTest()
        {
            this.cityDataMock = new Mock<ICityData>();
            this.cityBussiness = new CityBussiness(
                this.cityDataMock.Object);
        }
        [TestMethod]

        public void GetListWithRecords()
        {
            CityList citiesExpected = new CityList
            {
                Cities = new List<City>
                {
                    new City
                    {

                        Id = "5",
                        Name = "Tunja",
                        Code = "6060"
                    },
                    new City
                    {
                        Id = "6",
                        Name = "Honda",
                        Code = "3039"
                    },
                }
            };

            CityList citiesActual;

            this.cityDataMock.Setup(
                it => it.GetList()).Returns(new CityList
                {
                    Cities = new List<City>
                     {
                         new City
                    {

                        Id = "5",
                        Name = "Tunja",
                        Code = "6060"
                    },
                    new City
                    {

                        Id = "6",
                        Name = "Honda",
                        Code = "3039"
                    },
                     }
                });

            citiesActual = this.cityBussiness.GetList();

            Assert.AreEqual(citiesExpected.Cities.Count, citiesActual.Cities.Count);

        }

        [TestMethod]
        public void GetListWithoutRecords()
        {
            CityList citiesExpected = new CityList();
            CityList citiesActual;

            this.cityDataMock.Setup(
                it => it.GetList()).Returns(new CityList());

            citiesActual = this.cityBussiness.GetList();

            Assert.AreEqual(citiesExpected.Cities.Count, citiesActual.Cities.Count);
        }

        [TestMethod]
        public void AddSuccess()
        {
            City cityToAdd = new City
            {
                Id = "5",
                Name = "Tunja",
                Code = "6060"
            };
            this.cityDataMock.Setup(
                it => it.Add(It.IsAny<City>()));

            this.cityBussiness.Add(cityToAdd);

            Assert.IsTrue(true);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void AddWithoutId()
        {
            City cityToAdd = new City
            {
                Name = "Tunja",
                Code = "6060"
            };
            this.cityDataMock.Setup(
                it => it.Add(It.IsAny<City>()));

            this.cityBussiness.Add(cityToAdd);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void AddWithoutName()
        {
            City cityToAdd = new City
            {
                Id = "1",
                Code = "6060"
            };
            this.cityDataMock.Setup(
                it => it.Add(It.IsAny<City>()));

            this.cityBussiness.Add(cityToAdd);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void AddWithoutCode()
        {
            City cityToAdd = new City
            {
                Id = "5",
                Name = "Tunja"
            };
            this.cityDataMock.Setup(
                it => it.Add(It.IsAny<City>()));

            this.cityBussiness.Add(cityToAdd);
        }

        [TestMethod]
        public void UpdateSuccess()
        {
            City cityToUpadte = new City
            {
                Id = "5",
                Name = "Tunja",
                Code = "6060"
            };
            this.cityDataMock.Setup(
                it => it.Update(It.IsAny<City>()));

            this.cityBussiness.Update(cityToUpadte);

            Assert.IsTrue(true);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void UpdateWithoutId()
        {
            City cityToUpdate = new City
            {
                Name = "Tunja",
                Code = "6060"
            };
            this.cityDataMock.Setup(
                it => it.Update(It.IsAny<City>()));

            this.cityBussiness.Update(cityToUpdate);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void UpdateWithoutName()
        {
            City cityToUpdate = new City
            {
                Id = "5",
                Code = "6060"
            };
            this.cityDataMock.Setup(
                it => it.Update(It.IsAny<City>()));

            this.cityBussiness.Update(cityToUpdate);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void UpdateWithoutCode()
        {
            City cityToUpdate = new City
            {
                Id = "5",
                Name = "Tunja"
            };
            this.cityDataMock.Setup(
                it => it.Update(It.IsAny<City>()));

            this.cityBussiness.Update(cityToUpdate);
        }

        [TestMethod]
        public void DeleteSuccess()
        {
            string id = "5";

            this.cityDataMock.Setup(
                it => it.Delete(It.IsAny<string>()));

            this.cityBussiness.Delete(id);

            Assert.IsTrue(true);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void DeleteWithoutId()

        {
            string id = null;

            this.cityDataMock.Setup(
                it => it.Delete(It.IsAny<string>()));

            this.cityBussiness.Delete(id);
        }

        [TestMethod]
        public void GetSuccess()
        {
            string id = "5";

            this.cityDataMock.Setup(
                it => it.Get(It.IsAny<string>()));

            this.cityBussiness.Get(id);

            Assert.IsTrue(true);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GetWithoutId()

        {
            string id = null;

            this.cityDataMock.Setup(
                it => it.Get(It.IsAny<string>()));

            this.cityBussiness.Get(id);
        }

    }
}



