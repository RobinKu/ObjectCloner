using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ObjectCloner
{
    public class CloningMetaData
    {
        private readonly IMetaDataCollector collector;
        private readonly Type type;
        private readonly IDictionary<string, CloningPropertyMetaData> properties = new Dictionary<string, CloningPropertyMetaData>();
        private bool? clonable;

        public CloningMetaData(Type type, IMetaDataCollector collector)
        {
            if (type == null)
            {
                throw new ArgumentNullException("type");
            }
            else if (collector == null)
            {
                throw new ArgumentNullException("collector");
            }

            this.type = type;
            this.collector = collector;
        }

        public Type TargetType
        {
            get
            {
                return this.type;
            }
        }

        public bool Clonable
        {
            get
            {
                return this.clonable.GetValueOrDefault();
            }
            set
            {
                if (this.clonable.HasValue)
                {
                    throw new NotSupportedException("The Clonable property may only be set once (by the IMetaDataCollector implementation).");
                }
                else
                {
                    this.clonable = value;
                }
            }
        }

        public CloningPropertyMetaData GetMetaDataForProperty(string propertyName)
        {
            CloningPropertyMetaData metaData;

            if (!this.properties.TryGetValue(propertyName, out metaData))
            {
                metaData = this.collector.CreateMetaDataForProperty(this.type, propertyName);

                this.properties.Add(propertyName, metaData);
            }

            return metaData;
        }

        public IEnumerable<string> GetPropertyNames()
        {
            return this.collector.GetPropertyNames(this.type);
        }
    }
}
