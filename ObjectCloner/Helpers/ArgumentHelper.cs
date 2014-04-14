using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ObjectCloner.Helpers
{
    internal static class ArgumentHelper
    {
        internal static void ThrowExceptionIfNull(object value, string paramName)
        {
            if (value == null)
            {
                throw new ArgumentNullException(paramName);
            }
        }
    }
}
