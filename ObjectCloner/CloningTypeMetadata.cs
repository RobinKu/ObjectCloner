using ObjectCloner.Helpers;
using ObjectCloner.ObjectCreation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ObjectCloner
{
    public class CloningTypeMetadata
    {
        private readonly IMetadataCollector collector;
        private readonly Type type;
        private readonly IDictionary<string, CloningPropertyMetadata> properties = new Dictionary<string, CloningPropertyMetadata>();
        private readonly IFactory factory;
        private bool? clonable;

        public CloningTypeMetadata(Type type, IFactory factory, IMetadataCollector collector)
        {
            ArgumentHelper.ThrowExceptionIfNull(type, "type");
            ArgumentHelper.ThrowExceptionIfNull(factory, "factory");
            ArgumentHelper.ThrowExceptionIfNull(collector, "collector");

            this.type = type;
            this.factory = factory;
            this.collector = collector;
        }

        public Type TargetType
        {
            get
            {
                return this.type;
            }
        }

        public IFactory Factory
        {
            get
            {
                return this.factory;
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

        public CloningPropertyMetadata GetMetaDataForProperty(string propertyName)
        {
            CloningPropertyMetadata metaData;

            if (!this.properties.TryGetValue(propertyName, out metaData))
            {
                metaData = this.collector.CreateMetadataForProperty(this.type, propertyName);

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
