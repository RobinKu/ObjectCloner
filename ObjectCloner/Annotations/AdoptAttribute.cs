using ObjectCloner.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ObjectCloner.Annotations
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public class AdoptAttribute : CloningAttribute
    {
        public AdoptAttribute()
            : base(new AdoptValueCloningAction())
        {
        }
    }
}
