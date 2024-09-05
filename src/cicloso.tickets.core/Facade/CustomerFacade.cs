using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using cicloso.tickets.core.Bussiness;
using cicloso.tickets.entities;

namespace cicloso.tickets.core.Facade
{
    public class CustomerFacade
    {
        private CustomerBussiness customerBussiness;

        public CustomerFacade()
        {
            this.customerBussiness = new CustomerBussiness();
        }

        public CustomerList GetList()
        {
            CustomerList customers = this.customerBussiness.GetList();
            return customers;
        }

        public void Add(Customer customer)
        {
            this.customerBussiness.Add(customer);
        }
         
        public void Update(Customer customer)
        {
            this.customerBussiness.Update(customer);
        }

        public void Delete(string id)
        {
            this.customerBussiness.Delete(id);
        }
        
        public Customer Get(string id)
        {
            return this.customerBussiness.Get(id);
        }
    }
}
