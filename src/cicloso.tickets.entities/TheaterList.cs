using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cicloso.tickets.entities
{
    public class TheaterList
    {
        public TheaterList()
        {

            Theaters = new List<Theater>();
        }

        public List<Theater> Theaters { get; set; }

       
    }
}
