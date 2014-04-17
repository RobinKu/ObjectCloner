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
using System.Collections.Generic;

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
