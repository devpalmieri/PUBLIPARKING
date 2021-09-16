using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data
{
    public abstract class BaseEntity<EntityType> where EntityType : class
    {
        public EntityType Clone()
        {
            return (EntityType)this.MemberwiseClone();
        }
    }
}
