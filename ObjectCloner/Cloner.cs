using ObjectCloner.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ObjectCloner
{
    public static class Cloner
    {
        public static object Clone(this object value, IMetadataCollector collector)
        {
            CloneScope scope = new CloneScope(collector);

            return Clone(value, scope);
        }

        public static object Clone(this object value, CloneScope scope)
        {
            ArgumentHelper.ThrowExceptionIfNull(scope, "scope");

            return scope.Clone(value);
        }
    }
}
