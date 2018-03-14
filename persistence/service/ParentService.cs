using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using core.persistence;

namespace core.service
{
    public abstract class ParentService
    {
        public meteoEntities connexion;

        public ParentService()
        {
            connexion = new meteoEntities();

        }



    }
}
