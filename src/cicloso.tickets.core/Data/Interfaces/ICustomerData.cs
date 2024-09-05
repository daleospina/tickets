using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using cicloso.tickets.entities;

namespace cicloso.tickets.core.Data.Interfaces
{
    public interface ICustomerData
    {
        CustomerList GetList();

        void Add(Customer customer);

        void Update(Customer customer);

        void Delete(string id);

        Customer Get(string id);
    }
}
