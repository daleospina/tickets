using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using cicloso.tickets.core.Bussiness;
using cicloso.tickets.entities;


namespace cicloso.tickets.core.Facade
{
    public class CityFacade
    {
        private CityBussiness cityBussiness;

        public CityFacade()
        {
            this.cityBussiness = new CityBussiness();
        }

        public CityList GetList()
        {
            return this.cityBussiness.GetList();
        }

        public void Add(City city)
        {
            this.cityBussiness.Add(city);
        }

        public void Update(City city)
        {
            this.cityBussiness.Update(city);
        }

        public void Delete(string id)
        {
            this.cityBussiness.Delete(id);
        }

        public City Get(string id)
        {
            return this.cityBussiness.Get(id);
        }
    }
}
