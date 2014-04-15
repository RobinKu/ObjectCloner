using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ObjectCloner
{
    public interface ICloningAction
    {
        object Clone(CloneScope cloner, string propertyName, object valueFrom);
    }
}
