using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ObjectCloner.Actions
{
    public class StaticValueAction : ICloningAction
    {
        private readonly object value;

        public StaticValueAction(object value)
        {
            this.value = value;
        }

        public object Clone(Cloner cloner, string propertyName, object valueFrom)
        {
            return this.value;
        }
    }
}
