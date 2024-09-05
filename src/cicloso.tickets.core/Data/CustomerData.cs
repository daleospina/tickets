using cicloso.tickets.core.Data.Interfaces;
using cicloso.tickets.entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cicloso.tickets.core.Data
{
    internal class CustomerData : Base, ICustomerData
    {
        public CustomerList GetList()
        {
            CustomerList customers = new CustomerList();

            using (DbCommand command =
                this.Database.GetStoredProcCommand("uspgetlistcustomer"))
            {
                using (IDataReader dataReader = this.Database.ExecuteReader(command))
                {
                    while (dataReader.Read())
                    {
                        customers.Customers.Add(
                            new Customer
                            {
                                Id = dataReader["id"].ToString(),
                                FirstName = dataReader["firstname"].ToString(),
                                LastName = dataReader["lastname"].ToString()
                            }
                        );

                    }
                }
            }

            return customers;
        }

        public void Add(Customer customer)
        {
            using (DbCommand command =
                this.Database.GetStoredProcCommand("uspaddcustomer"))
            {

                this.Database.AddInParameter(command, "id", DbType.Int32, customer.Id);
                this.Database.AddInParameter(command, "firstname", DbType.String, customer.FirstName);
                this.Database.AddInParameter(command, "lastname", DbType.String, customer.LastName);

                this.Database.ExecuteNonQuery(command);
            }
        }

        public void Update(Customer customer)
        {
            using (DbCommand command =
                this.Database.GetStoredProcCommand("uspupdatecustomer"))
            {

                this.Database.AddInParameter(command, "id", DbType.Int32, customer.Id);
                this.Database.AddInParameter(command, "firstname", DbType.String, customer.FirstName);
                this.Database.AddInParameter(command, "lastname", DbType.String, customer.LastName);

                this.Database.ExecuteNonQuery(command);
            }

        }

        public void Delete(string id)
        {
            using (DbCommand command =
                this.Database.GetStoredProcCommand("uspdeletecustomer"))
            {

                this.Database.AddInParameter(command, "id", DbType.Int32, id);              

                this.Database.ExecuteNonQuery(command);
            }
        }
         
        public Customer Get(string id)
        {
            Customer customer = new Customer();

            using (DbCommand command =
                this.Database.GetStoredProcCommand("uspgetcustomer"))
            {
                this.Database.AddInParameter(command, "id", DbType.Int32, id);

                using (IDataReader dataReader = this.Database.ExecuteReader(command))
                {
                    while (dataReader.Read())
                    {
                        customer =
                            new Customer
                            {
                                Id = dataReader["id"].ToString(),
                                FirstName = dataReader["firstname"].ToString(),
                                LastName = dataReader["lastname"].ToString()
                            };                      
                    }
                }
            }

            return customer;

        }
    }
}
