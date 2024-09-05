using cicloso.tickets.core.Bussiness;
using cicloso.tickets.entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cicloso.tickets.core.Facade
{
   public class TheaterFacade
    {
        private TheaterBussiness theaterBussiness;

        public TheaterFacade()
        {
            this.theaterBussiness = new TheaterBussiness();
        }

        public TheaterList GetList()
        {
            TheaterList theaters = this.theaterBussiness.GetList();
            return theaters;
        }

        public void Add(Theater theater)
        {
            this.theaterBussiness.Add(theater);
        }

        public void Update(Theater theater)
        {
            this.theaterBussiness.Update(theater);
        }
        public void Delete(string id)
        {
            this.theaterBussiness.Delete(id);
        }

        public Theater Get(string id)
        {
            return this.theaterBussiness.Get(id);
        }


    }
}
