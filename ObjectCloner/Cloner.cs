using ObjectCloner.Helpers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ObjectCloner
{
    public class Cloner
    {
        private readonly IMetadataCollector collector;
        private readonly CloningMetadataCollection metadata;
        private readonly IDictionary<object, object> clonedObjects = new Dictionary<object, object>();

        public Cloner(IMetadataCollector collector)
        {
            ArgumentHelper.ThrowExceptionIfNull(collector, "collector");

            this.collector = collector;
            this.metadata = new CloningMetadataCollection(collector);
        }

        public CloningMetadataCollection Metadata
        {
            get
            {
                return this.metadata;
            }
        }

        public object Clone(object obj)
        {
            object clone = null;

            if (obj != null)
            {
                if (!this.clonedObjects.TryGetValue(obj, out clone))
                {
                    Type sourceType = obj.GetType();

                    CloningMetadata typeMetadata = this.Metadata[sourceType];

                    ThrowExceptionIfNotClonable(typeMetadata);

                    clone = CreateNewInstance(typeMetadata);
                    this.clonedObjects.Add(obj, clone);

                    IDictionary<string, object> clonedPropertyValues = CloneProperties(obj, typeMetadata);
                    SetNewPropertyValues(clonedPropertyValues, clone);
                }
            }

            return clone;
        }

        private void ThrowExceptionIfNotClonable(CloningMetadata typeMetadata)
        {
            if (!typeMetadata.Clonable)
            {
                throw new InvalidOperationException(string.Format("The type {0} is not clonable (e.g. the MetadataCollector (of type {1}) could not provider any metadata)", typeMetadata.TargetType, this.collector.GetType()));
            }
        }

        private IDictionary<string, object> CloneProperties(object obj, CloningMetadata typeMetadata)
        {
            IDictionary<string, object> clonedPropertyValues = new Dictionary<string, object>();

            foreach (string propertyName in typeMetadata.GetPropertyNames())
            {
                CloningPropertyMetadata propertyMetadata = typeMetadata.GetMetaDataForProperty(propertyName);

                object clonedValue = propertyMetadata.Action.Clone(this, propertyName, obj);

                clonedPropertyValues.Add(propertyName, clonedValue);
            }

            return clonedPropertyValues;
        }

        private static object CreateNewInstance(CloningMetadata typeMetadata)
        {
            return typeMetadata.Factory.CreateNew(typeMetadata);
        }

        private static void SetNewPropertyValues(IDictionary<string, object> clonedPropertyValues, object newValue)
        {
            foreach (KeyValuePair<string, object> property in clonedPropertyValues)
            {
                ReflectionHelper.SetPropertyValue(newValue, property.Key, property.Value);
            }
        }
    }
}
