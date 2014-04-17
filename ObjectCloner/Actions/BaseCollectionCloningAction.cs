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
using System.Collections;
using System.Collections.Generic;

namespace ObjectCloner.Actions
{
    public abstract class BaseCollectionCloningAction : ICloningAction
    {
        public object Clone(CloneScope cloner, string propertyName, object valueFrom)
        {
            object propertyValue = ReflectionHelper.GetPropertyValue(valueFrom, propertyName);

            IList listValue = (IList)propertyValue;
            IList newList = new List<object>();

            for (int i = 0; i < listValue.Count; i++)
            {
                newList.Add(GetItemValue(cloner, listValue, i));
            }

            return newList;
        }

        protected abstract object GetItemValue(CloneScope cloner, IList originalList, int index);
    }
}
