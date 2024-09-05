using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cicloso.tickets.entities
{
    public class CityList
    {
        public CityList()
        {
            Cities = new List<City>();
        }

        public List<City> Cities { get; set; }
    }
}
