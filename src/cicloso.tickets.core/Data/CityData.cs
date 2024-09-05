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
    internal class CityData : Base, ICityData
    {
        public CityList GetList()
        {
            CityList cities = new CityList();
            
            using (DbCommand command =
                this.Database.GetStoredProcCommand("uspgetlistcity"))
            {
                using (IDataReader dataReader = this.Database.ExecuteReader(command))
                {
                    while (dataReader.Read())
                    {
                        cities.Cities.Add(
                            new City
                            {
                                Id = dataReader["id"].ToString(),
                                Name = dataReader["name"].ToString(),
                                Code = dataReader["code"].ToString()
                            }
                        );                     
                    }
                }
            }
            return cities;
        }

        public void Add(City city)
        {
            using (DbCommand command =
                this.Database.GetStoredProcCommand("uspaddcity"))
            {
                this.Database.AddInParameter(command, "id", DbType.Int32, city.Id);
                this.Database.AddInParameter(command, "name", DbType.String, city.Name);
                this.Database.AddInParameter(command, "code", DbType.String, city.Code);

                this.Database.ExecuteNonQuery(command);
            }

        }

        public void Update(City city)
        {
            using (DbCommand command =
                this.Database.GetStoredProcCommand("uspupdatecity"))
            {

                this.Database.AddInParameter(command, "id", DbType.Int32, city.Id);
                this.Database.AddInParameter(command, "name", DbType.String, city.Name);
                this.Database.AddInParameter(command, "code", DbType.String, city.Code);

                this.Database.ExecuteNonQuery(command);
            }

        }

        public void Delete(string id)
        {
            using (DbCommand command =
                this.Database.GetStoredProcCommand("uspdeletecity"))
            {

                this.Database.AddInParameter(command, "id", DbType.Int32, id);

                this.Database.ExecuteNonQuery(command);
            }
        }

        public City Get(string id)
        {
            City city = new City();

            using (DbCommand command =
                this.Database.GetStoredProcCommand("uspgetcity"))
            {
                this.Database.AddInParameter(command, "id", DbType.Int32, id);

                using (IDataReader dataReader = this.Database.ExecuteReader(command))
                {
                    while (dataReader.Read())
                    {
                        city =
                            new City
                            {
                                Id = dataReader["id"].ToString(),
                                Name = dataReader["name"].ToString(),
                                Code = dataReader["code"].ToString()
                            };
                    }
                }
            }

            return city;

        }
    }
}