using ObjectCloner.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ObjectCloner.Actions
{
    public class CloneValueCloningAction : ICloningAction
    {
        public object Clone(CloneScope cloner, string propertyName, object valueFrom)
        {
            object propertyValue = ReflectionHelper.GetPropertyValue(valueFrom, propertyName);

            return cloner.Clone(propertyValue);
        }
    }
}
