using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cicloso.tickets.entities
{
   public class Theater
    {
        public Theater()
        {
            Cities = new CityList();
        }

        public string Id { get; set; }
        
        public string IdCity { get; set; }

        public string Name { get; set; }
        
        public string Address { get; set; }
        
        public  string State { get; set; }

        public CityList Cities { get; set; }
    }
}
