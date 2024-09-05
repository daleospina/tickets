using cicloso.tickets.entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cicloso.tickets.core.Data.Interfaces
{
    public interface ITheaterData
    {
        TheaterList GetList();

        void Add(Theater theater);

        void Update(Theater theater);

        void Delete(string id);

        Theater Get(string id);

    }
}
