using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ObjectCloner
{
    public class Cloner
    {
        private readonly IMetaDataCollector collector;
        private readonly CloningMetaDataCollection metaData;

        public Cloner(IMetaDataCollector collector)
        {
            if (collector == null)
            {
                throw new ArgumentNullException("collector");
            }

            this.collector = collector;
            this.metaData = new CloningMetaDataCollection(collector);
        }

        public CloningMetaDataCollection MetaData
        {
            get
            {
                return this.metaData;
            }
        }

        public object Clone(object obj)
        {
            object clone = null;

            if (obj != null)
            {
                Type sourceType = obj.GetType();

                CloningMetaData typeMetaData = this.MetaData[sourceType];

                if (typeMetaData.Clonable)
                {
                    IDictionary<string, object> clonedPropertyValues = CloneProperties(obj, typeMetaData);

                    object newObject = CreateNewInstance(typeMetaData);

                    SetNewPropertyValues(clonedPropertyValues, newObject);

                    return newObject;
                }
                else
                {
                    throw new InvalidOperationException(string.Format("The type {0} is not clonable (e.g. the MetaDataCollector (of type {1}) could not provider any meta data)", sourceType, this.collector.GetType()));
                }
            }

            return clone;
        }

        private IDictionary<string, object> CloneProperties(object obj, CloningMetaData typeMetaData)
        {
            IDictionary<string, object> clonedPropertyValues = new Dictionary<string, object>();

            foreach (string propertyName in typeMetaData.GetPropertyNames())
            {
                CloningPropertyMetaData propertyMetaData = typeMetaData.GetMetaDataForProperty(propertyName);

                object clonedValue = propertyMetaData.Action.Clone(this, propertyName, obj);

                clonedPropertyValues.Add(propertyName, clonedValue);
            }

            return clonedPropertyValues;
        }

        private static object CreateNewInstance(CloningMetaData typeMetaData)
        {
            object newValue = Activator.CreateInstance(typeMetaData.TargetType);

            return newValue;
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
