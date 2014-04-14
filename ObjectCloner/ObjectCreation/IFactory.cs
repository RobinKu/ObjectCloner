using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ObjectCloner.ObjectCreation
{
    public interface IFactory
    {
        object CreateNew(CloningMetadata metadata);
    }
}
