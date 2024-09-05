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
    public class TheaterTest
    {
        private Mock<ITheaterData> theaterDataMock = null;
        private TheaterBussiness theaterBussiness = null;

        [TestInitialize]

        public void InitializeTest()
        {
            this.theaterDataMock = new Mock<ITheaterData>();
            this.theaterBussiness = new TheaterBussiness(
                this.theaterDataMock.Object);
        }
        [TestMethod]

        public void GetListWithRecords()
        {

            TheaterList theaterExpected = new TheaterList
            {
                Theaters = new List<Theater>
                {
                    new Theater
                    {
                        Id = "1",
                        IdCity = "6060",
                        Name = "joe",
                        Address = "cra 95",
                        State = "0"
                    },
                    new Theater
                    {
                        Id = "2",
                        IdCity = "8585",
                        Name = "Uribe",
                        Address = "cra 123",
                        State = "1"
                    },
                }
            };

            TheaterList theaterActual;

            this.theaterDataMock.Setup(it => it.GetList()).Returns(new TheaterList
            {
                Theaters = new List<Theater>
                {
                    new Theater
                    {
                         Id = "1",
                        IdCity = "6060",
                        Name = "joe",
                        Address = "cra 95",
                        State = "0"
                    },
                    new Theater
                    {
                        Id = "2",
                        IdCity = "8585",
                        Name = "Uribe",
                        Address = "cra 123",
                        State = "1"
                    },
                }
            });
            theaterActual = this.theaterBussiness.GetList();

            Assert.AreEqual(theaterExpected.Theaters.Count, theaterActual.Theaters.Count);
        }

        [TestMethod]
        public void AddSuccess()
        {
            Theater theaterToAdd = new Theater
            {
                Id = "1",
                IdCity = "6060",
                Name = "joe",
                Address = "cra 95",
                State = "0"
            };
            this.theaterDataMock.Setup(
              it => it.Add(It.IsAny<Theater>()));

            this.theaterBussiness.Add(theaterToAdd);

            Assert.IsTrue(true);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void AddWithoutId()
        {
            Theater theaterToAdd = new Theater
            {
                IdCity = "6060",
                Name = "joe",
                Address = "cra 95",
                State = "0"
            };
            this.theaterDataMock.Setup(
                it => it.Add(It.IsAny<Theater>()));

            this.theaterBussiness.Add(theaterToAdd);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void AddWithoutIdCity()
        {
            Theater theaterToAdd = new Theater
            {
                Id = "1",

                Name = "joe",
                Address = "cra 95",
                State = "0"
            };
            this.theaterDataMock.Setup(
                it => it.Add(It.IsAny <Theater>()));

            this.theaterBussiness.Add(theaterToAdd);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void AddWithoutName()
        {
            Theater theaterToAdd = new Theater
            {
                Id = "1",
                IdCity = "6060",
               
                Address = "cra 95",
                State = "0"
            };
            this.theaterDataMock.Setup(
                it => it.Add(It.IsAny<Theater>()));

            this.theaterBussiness.Add(theaterToAdd);
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void AddWithoutAddress()
        {
            Theater theaterToAdd = new Theater
            {
                Id = "1",
                IdCity = "6060",
                Name = "joe",

                State = "0"
            };
            this.theaterDataMock.Setup(
                it => it.Add(It.IsAny<Theater>()));

            this.theaterBussiness.Add(theaterToAdd);
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void AddWithoutState()
        {
            Theater theaterToAdd = new Theater
            {
                Id = "1",
                IdCity = "6060",
                Name = "joe",
                Address = "cra 95"


            };
            this.theaterDataMock.Setup(
                it => it.Add(It.IsAny<Theater>()));

            this.theaterBussiness.Add(theaterToAdd);
        }

        [TestMethod]
        public void UpdateSucces()
        {
            Theater theaterToUpdate = new Theater
            {
                Id = "1",
                IdCity = "6060",
                Name = "joe",
                Address = "cra 95",
                State = "0"
            };
            this.theaterDataMock.Setup(
                it => it.Update(It.IsAny<Theater>()));

            this.theaterBussiness.Update(theaterToUpdate);

            Assert.IsTrue(true);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void UpdateWithoutId()
        {
            Theater theaterToUpdate = new Theater
            {
               
                IdCity = "6060",
                Name = "joe",
                Address = "cra 95",
                State = "0"
            };
            this.theaterDataMock.Setup(
                it => it.Update(It.IsAny<Theater>()));

            this.theaterBussiness.Update(theaterToUpdate);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void UpdateWithoutIdCity()
        {
            Theater theaterToUpdate = new Theater
            {
                Id = "1",
               
                Name = "joe",
                Address = "cra 95",
                State = "0"
            };
            this.theaterDataMock.Setup(
                it => it.Update(It.IsAny<Theater>()));

            this.theaterBussiness.Update(theaterToUpdate);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void UpdateWithoutName()
        {
            Theater theaterToUpdate = new Theater
            {
                Id = "1",
                IdCity = "6060",
               
                Address = "cra 95",
                State = "0"
            };
            this.theaterDataMock.Setup(
                it => it.Update(It.IsAny<Theater>()));

            this.theaterBussiness.Update(theaterToUpdate);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void UpdateWithoutAdress()
        {
            Theater theaterToUpdate = new Theater
            {
                Id = "1",
                IdCity = "6060",
                Name = "joe",
                
                State = "0"
            };
            this.theaterDataMock.Setup(
                it => it.Update(It.IsAny<Theater>()));

            this.theaterBussiness.Update(theaterToUpdate);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void UpdateWithoutState()
        {
            Theater theaterToUpdate = new Theater
            {
                Id = "1",
                IdCity = "6060",
                Name = "joe",
                Address = "cra 95",
            };
            this.theaterDataMock.Setup(
                it => it.Update(It.IsAny<Theater>()));

            this.theaterBussiness.Update(theaterToUpdate);
        }

        [TestMethod]
        public void DeleteSuccess()
        {
            string id = "5";

            this.theaterDataMock.Setup(
                it => it.Delete(It.IsAny<string>()));

            this.theaterBussiness.Delete(id);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void DeleteWithoutId()

        {
            string id = null;

            this.theaterDataMock.Setup(
                it => it.Delete(It.IsAny<string>()));

            this.theaterBussiness.Delete(id);
        }

        [TestMethod]
        public void GetSuccess()
        {
            string id = "5";

            this.theaterDataMock.Setup(
                it => it.Get(It.IsAny<string>()));

            this.theaterBussiness.Get(id);

            Assert.IsTrue(true);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GetWithoutId()

        {
            string id = null;

            this.theaterDataMock.Setup(
                it => it.Get(It.IsAny<string>()));

            this.theaterBussiness.Get(id);
        }

    }  
}
