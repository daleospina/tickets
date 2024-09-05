using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace cicloso.tickets.core.Data
{
    internal abstract class Base
    {
        public readonly Database Database;

        protected Base()
        {
            DatabaseProviderFactory databaseProviderFactory = new DatabaseProviderFactory();
            this.Database = databaseProviderFactory.CreateDefault();
        }
    }
}
