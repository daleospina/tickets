using cicloso.tickets.core.Data;
using cicloso.tickets.core.Data.Interfaces;
using cicloso.tickets.entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cicloso.tickets.core.Bussiness
{
    public class CustomerBussiness
    {
        private ICustomerData customerData;

        public CustomerBussiness() : 
            this (new CustomerData())
        {
            
        }

        public CustomerBussiness(ICustomerData customerData)
        {
            this.customerData = customerData;
        }

        public CustomerList GetList()
        {
            CustomerList customer = this.customerData.GetList();
            return customer;
        }

        public void Add(Customer customer)
        {
            if (string.IsNullOrEmpty(customer.Id))
            {
                throw new ArgumentNullException();
            }

            if (string.IsNullOrEmpty(customer.FirstName))
            {
                throw new ArgumentNullException();
            }

            if (string.IsNullOrEmpty(customer.LastName))
            {
                throw new ArgumentNullException();
            }

            this.customerData.Add(customer);
        }

        public void Update(Customer customer)
        {
            if (string.IsNullOrEmpty(customer.Id))
            {
                throw new ArgumentNullException();
            }

            if (string.IsNullOrEmpty(customer.FirstName))
            {
                throw new ArgumentNullException();
            }

            if (string.IsNullOrEmpty(customer.LastName))
            {
                throw new ArgumentNullException();
            }

            this.customerData.Update(customer);
        }

        public void Delete(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new ArgumentNullException();
            }

            this.customerData.Delete(id);
        }

        public Customer Get(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new ArgumentNullException();
            }
            
            return this.customerData.Get(id);            
        }
    }
}
