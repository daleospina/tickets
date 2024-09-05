using System;
using System.Collections.Generic;
using System.Linq;

namespace cicloso.tickets.entities
{
    public class CustomerList
    {
        public CustomerList()
        {
            Customers = new List<Customer>();
        }

        public List<Customer> Customers { get; set; }
    }
}