using cicloso.tickets.core.Bussiness;
using cicloso.tickets.core.Data.Interfaces;
using cicloso.tickets.entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;

namespace cicloso.tickets.tests
{
    [TestClass]
    public class CustomerTest
    {
        private Mock<ICustomerData> customerDataMock = null;
        private CustomerBussiness customerBussiness = null;

        [TestInitialize]
        public void InitializeTest()
        {
            this.customerDataMock = new Mock<ICustomerData>();
            this.customerBussiness = new CustomerBussiness(
                this.customerDataMock.Object);
        }

        [TestMethod]
        public void GetListWithRecords()
        {
            CustomerList customersExpected = new CustomerList
            {
                Customers = new List<Customer>
                {
                    new Customer
                    {
                        Id = "1",
                        FirstName = "Pedro",
                        LastName = "Perez"   
                    },
                    new Customer
                    {
                        Id = "2",
                        FirstName = "Maria",
                        LastName = "Moreno"
                    }
                }   
            };
            CustomerList customerActual;

            this.customerDataMock.Setup(
                it => it.GetList()).Returns(new CustomerList
                {
                    Customers = new List<Customer>
                    {
                        new Customer
                        {
                            Id = "1",
                            FirstName = "Pedro",
                            LastName = "Perez"
                        },
                        new Customer
                        {
                            Id = "2",
                            FirstName = "Maria",
                            LastName = "Moreno"
                        }
                    }
                });
            customerActual = this.customerBussiness.GetList();

            Assert.AreEqual(customersExpected.Customers.Count, customerActual.Customers.Count);
        }

        [TestMethod]
        public void GetListWithoutRecords()
        {
            CustomerList customersExpected = new CustomerList();
            CustomerList customerActual;

            this.customerDataMock.Setup(
                it => it.GetList()).Returns(new CustomerList());
            customerActual = this.customerBussiness.GetList();

            Assert.AreEqual(customersExpected.Customers.Count, customerActual.Customers.Count);
        }

        [TestMethod]
        public void AddSuccess()
        {
            Customer customerToAdd = new Customer
            {
                Id = "1",
                FirstName = "Pedro",
                LastName = "Perez"
            };
            this.customerDataMock.Setup(
                it => it.Add(It.IsAny<Customer>()));

            this.customerBussiness.Add(customerToAdd);

            Assert.IsTrue(true);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void AddWithoutId()
        {
            Customer customerToAdd = new Customer
            {
                FirstName = "Pedro",
                LastName = "Perez"
            };
            this.customerDataMock.Setup(
                it => it.Add(It.IsAny<Customer>()));

            this.customerBussiness.Add(customerToAdd);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void AddWithoutFirstName()
        {
            Customer customerToAdd = new Customer
            {
                Id = "1",
                LastName = "Perez"
            };
            this.customerDataMock.Setup(
                it => it.Add(It.IsAny<Customer>()));

            this.customerBussiness.Add(customerToAdd);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void AddWithoutLastName()
        {
            Customer customerToAdd = new Customer
            {
                Id = "1",
                FirstName = "Pedro"
            };
            this.customerDataMock.Setup(
                it => it.Add(It.IsAny<Customer>()));

            this.customerBussiness.Add(customerToAdd);
        }

        [TestMethod]
        public void UpdateSuccess()
        {
            Customer customerToUpdate = new Customer
            {
                Id = "1",
                FirstName = "Pedro",
                LastName = "Perez"
            };
            this.customerDataMock.Setup(
                it => it.Update(It.IsAny<Customer>()));

            this.customerBussiness.Update(customerToUpdate);

            Assert.IsTrue(true);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void UpdateWithoutId()
        {
            Customer customerToUpdate = new Customer
            {
                FirstName = "Pedro",
                LastName = "Perez"
            };
            this.customerDataMock.Setup(
                it => it.Update(It.IsAny<Customer>()));

            this.customerBussiness.Update(customerToUpdate);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void UpdateWithoutFirstName()
        {
            Customer customerToUpdate = new Customer
            {
                Id = "1",
                LastName = "Perez"
            };
            this.customerDataMock.Setup(
                it => it.Update(It.IsAny<Customer>()));

            this.customerBussiness.Update(customerToUpdate);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void UpdateWithoutLastName()
        {
            Customer customerToUpdate = new Customer
            {
                Id = "1",
                FirstName = "Pedro"
            };
            this.customerDataMock.Setup(
                it => it.Update(It.IsAny<Customer>()));

            this.customerBussiness.Update(customerToUpdate);
        }

        [TestMethod]
        public void DeleteSuccess()
        {
            string id = "1";
           
            this.customerDataMock.Setup(
                it => it.Delete(It.IsAny<string>()));
            
            this.customerBussiness.Delete(id);

            Assert.IsTrue(true);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void DeleteWithoutId()

        {
            string id = null;

            this.customerDataMock.Setup(
                it => it.Delete(It.IsAny<string>()));

            this.customerBussiness.Delete(id);
        }

        [TestMethod]
        public void GetSuccess()
        {
            string id = "1";

            this.customerDataMock.Setup(
                it => it.Get(It.IsAny<string>()));

            this.customerBussiness.Get(id);

            Assert.IsTrue(true);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GetWithoutId()

        {
            string id = null;

            this.customerDataMock.Setup(
                it => it.Get(It.IsAny<string>()));

            this.customerBussiness.Get(id);
        }

    }
}
 