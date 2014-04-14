using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ObjectCloner.ObjectCreation
{
    public class DefaultConstructorFactory : IFactory
    {
        public object CreateNew(CloningMetadata metadata)
        {
            return Activator.CreateInstance(metadata.TargetType);
        }
    }
}
