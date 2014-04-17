using ObjectCloner.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ObjectCloner.Annotations
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public class CloneAttribute : CloningAttribute
    {
        public CloneAttribute()
            : base(new CloneValueCloningAction())
        {
        }
    }
}
