using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ObjectCloner
{
    public class CloningMetaDataCollection
    {
        private readonly IMetaDataCollector collector;
        private readonly IDictionary<Type, CloningMetaData> metaDataPerType = new Dictionary<Type, CloningMetaData>();

        public CloningMetaDataCollection(IMetaDataCollector collector)
        {
            if (collector == null)
            {
                throw new ArgumentNullException("collector");
            }

            this.collector = collector;
        }

        public CloningMetaData this[Type type]
        {
            get
            {
                CloningMetaData metaData;

                if (!this.metaDataPerType.TryGetValue(type, out metaData))
                {
                    metaData = this.collector.CreateMetaDataForType(type);
                }

                return metaData;
            }
        }
    }
}
