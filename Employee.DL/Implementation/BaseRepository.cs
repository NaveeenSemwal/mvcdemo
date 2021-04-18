using Employees.DL.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employees.DL.Implementation
{
    /// <summary>
    /// TODO : https://techbrij.com/generic-repository-unit-of-work-entity-framework-unit-testing-asp-net-mvc
    /// 
    /// https://www.programmingwithwolfgang.com/repository-and-unit-of-work-pattern/
    /// </summary>
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