using ObjectCloner.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ObjectCloner.Actions
{
    public class AdoptValueCloningAction : ICloningAction
    {
        public object Clone(CloneScope cloner, string propertyName, object valueFrom)
        {
            return ReflectionHelper.GetPropertyValue(valueFrom, propertyName);
        }
    }
}
