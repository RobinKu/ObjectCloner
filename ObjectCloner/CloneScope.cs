/*
 * ObjectCloner
 * Copyright (C) 2014 Robin Kuijstermans
 * 
 * This program is free software: you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * (at your option) any later version.
 * 
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 * 
 * You should have received a copy of the GNU General Public License
 * along with this program.  If not, see [http://www.gnu.org/licenses/].
 */
using ObjectCloner.Helpers;
using System;
using System.Collections;
using System.Collections.Generic;

namespace ObjectCloner
{
    public class CloneScope
    {
        private readonly IMetadataCollector collector;
        private readonly CloningMetadataCollection metadata;
        private readonly IDictionary<object, object> clonedObjects = new Dictionary<object, object>();

        public CloneScope(IMetadataCollector collector)
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

            if (obj != null && !this.clonedObjects.TryGetValue(obj, out clone))
            {
                Type sourceType = obj.GetType();

                CloningTypeMetadata typeMetadata = this.Metadata.GetMetadataForType(sourceType);

                ThrowExceptionIfNotClonable(typeMetadata);

                clone = CreateNewInstance(typeMetadata);
                this.clonedObjects.Add(obj, clone);

                IDictionary<string, object> clonedPropertyValues = CloneProperties(obj, typeMetadata);
                SetNewPropertyValues(clonedPropertyValues, clone);
            }

            return clone;
        }

        private void ThrowExceptionIfNotClonable(CloningTypeMetadata typeMetadata)
        {
            if (!typeMetadata.Clonable)
            {
                throw new InvalidOperationException(string.Format("The type {0} is not clonable (e.g. the MetadataCollector (of type {1}) could not provider any metadata)", typeMetadata.TargetType, this.collector.GetType()));
            }
        }

        private IDictionary<string, object> CloneProperties(object obj, CloningTypeMetadata typeMetadata)
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

        private static object CreateNewInstance(CloningTypeMetadata typeMetadata)
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
