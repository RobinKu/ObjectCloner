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
using ObjectCloner.ObjectCreation;
using System;
using System.Collections.Generic;

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
