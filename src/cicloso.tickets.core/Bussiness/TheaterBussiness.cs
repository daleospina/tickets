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
    public class TheaterBussiness
    {
        private ITheaterData theaterData;

        public TheaterBussiness() :
            this(new TheaterData())
        {

        }
        public TheaterBussiness(ITheaterData theaterData)
        {
            this.theaterData = theaterData;
        }

        public TheaterList GetList()
        {
            TheaterList theater = this.theaterData.GetList();
            return theater;
        }

        public void Add(Theater theater)
        {
            if (string.IsNullOrEmpty(theater.Id))
            {
                throw new ArgumentNullException();
            }

            if (string.IsNullOrEmpty(theater.IdCity))
            {
                throw new ArgumentNullException();
            }

            if (string.IsNullOrEmpty(theater.Name))
            {
                throw new ArgumentNullException();
            }
            if (string.IsNullOrEmpty(theater.Address))
            {
                throw new ArgumentNullException();
            }

            if (string.IsNullOrEmpty(theater.State))
            {
                throw new ArgumentNullException();
            }

            this.theaterData.Add(theater);
        }

        public void Update(Theater theater)
        {
            if (string.IsNullOrEmpty(theater.Id))
            {
                throw new ArgumentNullException();
            }

            if (string.IsNullOrEmpty(theater.IdCity))
            {
                throw new ArgumentNullException();
            }

            if (string.IsNullOrEmpty(theater.Name))
            {
                throw new ArgumentNullException();
            }

            if (string.IsNullOrEmpty(theater.Address))
            {
                throw new ArgumentNullException();
            }

            if (string.IsNullOrEmpty(theater.State))
            {
                throw new ArgumentNullException();
            }

            this.theaterData.Update(theater);
        }

        public void Delete(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new ArgumentNullException();
            }

            this.theaterData.Delete(id);
        }

        public Theater Get(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new ArgumentNullException();
            }

            return this.theaterData.Get(id);
        }
    }
}
