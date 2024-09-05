using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using cicloso.tickets.entities;

namespace cicloso.tickets.core.Data.Interfaces
{
    public interface ICityData
    {
        CityList GetList();

        void Add(City city);

        void Update(City city);

        void Delete(string id);

        City Get(string id);      
    }
}
