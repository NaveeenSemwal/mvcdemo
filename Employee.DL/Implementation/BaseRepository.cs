using Employees.DL.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employees.DL.Implementation
{
    public abstract class BaseRepository
    {
        private static readonly object padlock = new object();

        private static EMSDemoEntities _dbContext = null;

        protected static EMSDemoEntities DbContext
        {
            get
            {
                if (_dbContext == null)
                {
                    lock (padlock)
                    {
                        if (_dbContext == null)
                        {
                            _dbContext = new EMSDemoEntities();
                        }
                    }
                }

                _dbContext.Database.CommandTimeout = 180;

                return _dbContext;
            }
        }
    }
}