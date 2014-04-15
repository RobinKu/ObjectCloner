using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ObjectCloner
{
    public interface IMetadataCollector
    {
        CloningTypeMetadata CreateMetadataForType(Type type);

        CloningPropertyMetadata CreateMetadataForProperty(Type type, string propertyName);

        IEnumerable<string> GetPropertyNames(Type type);
    }
}
