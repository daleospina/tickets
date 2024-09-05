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
    public class CityBussiness
    {
        private ICityData cityData;

        public CityBussiness() :
            this(new CityData())
        {

        }

        public CityBussiness(ICityData cityData)
        {
            this.cityData = cityData;
        }

        public CityList GetList()
        {
            CityList cities = this.cityData.GetList();            
            return cities;
        } 

        public void Add(City city)
        {
            if (string.IsNullOrEmpty(city.Id))
            {
                throw new ArgumentNullException();
            }

            if (string.IsNullOrEmpty(city.Name))
            {
                throw new ArgumentNullException();
            }

            if (string.IsNullOrEmpty(city.Code))
            {
                throw new ArgumentNullException();
            }

            this.cityData.Add(city);
        }

        public void Update(City city)
        {
            if (string.IsNullOrEmpty(city.Id))
            {
                throw new ArgumentNullException();
            }

            if (string.IsNullOrEmpty(city.Name))
            {
                throw new ArgumentNullException();
            }

            if (string.IsNullOrEmpty(city.Code))
            {
                throw new ArgumentNullException();
            }

            this.cityData.Update(city);
        }

        public void Delete(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new ArgumentNullException();
            }

            this.cityData.Delete(id);
        }

        public City Get(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new ArgumentNullException();
            }

            return this.cityData.Get(id);
        }
    }
}
