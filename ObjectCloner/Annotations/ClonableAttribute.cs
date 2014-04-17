using ObjectCloner.ObjectCreation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ObjectCloner.Annotations
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public class ClonableAttribute : Attribute
    {
        public ClonableAttribute()
        {
        }

        public Type FactoryType
        {
            get;
            set;
        }
    }
}
