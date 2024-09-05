using cicloso.tickets.core.Data.Interfaces;
using cicloso.tickets.entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cicloso.tickets.core.Data
{
    internal class TheaterData : Base, ITheaterData
    {
        public TheaterList GetList()
        {
            TheaterList theaters = new TheaterList();

            using (DbCommand command = this.Database.GetStoredProcCommand("uspgetlisttheater"))
            {
                using (IDataReader dataReader = this.Database.ExecuteReader(command))
                {
                    while (dataReader.Read())
                    {
                        theaters.Theaters.Add(

                            new Theater
                            {
                                Id = dataReader["id"].ToString(),
                                IdCity = dataReader["idcity"].ToString(),
                                Name = dataReader["name"].ToString(),
                                Address = dataReader["address"].ToString(),
                            }
                        );

                    }
                }
            }

            return theaters;
        }

        public void Add(Theater theater)
        {
            using (DbCommand command =
                this.Database.GetStoredProcCommand("uspaddtheater"))
            {

                this.Database.AddInParameter(command, "id", DbType.Int32, theater.Id);
                this.Database.AddInParameter(command, "idcity", DbType.String, theater.IdCity);
                this.Database.AddInParameter(command, "name", DbType.String, theater.Name);
                this.Database.AddInParameter(command, "address", DbType.String, theater.Address);
                this.Database.AddInParameter(command, "state", DbType.String, theater.State);

                this.Database.ExecuteNonQuery(command);
            }
        }

        public void Update(Theater theater)
        {
            using (DbCommand command =
                this.Database.GetStoredProcCommand("uspupdatetheater"))
            {

                this.Database.AddInParameter(command, "id", DbType.Int32, theater.Id);
                this.Database.AddInParameter(command, "idcity", DbType.String, theater.IdCity);
                this.Database.AddInParameter(command, "name", DbType.String, theater.Name);
                this.Database.AddInParameter(command, "address", DbType.String, theater.Address);
                this.Database.AddInParameter(command, "state", DbType.String, theater.State);

                this.Database.ExecuteNonQuery(command);
            }
        }

        public void Delete(string id)
        {
            using (DbCommand command =
                this.Database.GetStoredProcCommand("uspdeletetheater"))
            {

                this.Database.AddInParameter(command, "id", DbType.Int32, id);

                this.Database.ExecuteNonQuery(command);
            }
        }

        public Theater Get(string id)
        {
            Theater theater = new Theater();

            using (DbCommand command =

                this.Database.GetStoredProcCommand("uspgettheater"))
            {
                this.Database.AddInParameter(command, "id", DbType.Int32, id);

                using (IDataReader dataReader = this.Database.ExecuteReader(command))

                {
                    while (dataReader.Read())

                    {
                        theater =
                            new Theater
                            {
                                Id = dataReader["id"].ToString(),
                                IdCity = dataReader["idcity"].ToString(),
                                Name = dataReader["name"].ToString(),
                                Address = dataReader["address"].ToString(),
                                State = dataReader["state"].ToString(),
                            };
                    }
                }
            }

            return theater;

        }

    }
}
