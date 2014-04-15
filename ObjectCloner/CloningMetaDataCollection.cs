using ObjectCloner.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ObjectCloner
{
    public class CloningMetadataCollection
    {
        private readonly IMetadataCollector collector;
        private readonly IDictionary<Type, CloningTypeMetadata> metadataPerType = new Dictionary<Type, CloningTypeMetadata>();

        public CloningMetadataCollection(IMetadataCollector collector)
        {
            ArgumentHelper.ThrowExceptionIfNull(collector, "collector");

            this.collector = collector;
        }

        public CloningTypeMetadata GetMetadataForType(Type type)
        {
            CloningTypeMetadata metadata;

            if (!this.metadataPerType.TryGetValue(type, out metadata))
            {
                metadata = this.collector.CreateMetadataForType(type);
            }

            return metadata;
        }
    }
}
