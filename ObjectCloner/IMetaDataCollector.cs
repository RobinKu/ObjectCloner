using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ObjectCloner
{
    public interface IMetaDataCollector
    {
        CloningMetaData CreateMetaDataForType(Type type);

        CloningPropertyMetaData CreateMetaDataForProperty(Type type, string propertyName);

        IEnumerable<string> GetPropertyNames(Type type);
    }
}
