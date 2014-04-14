using ObjectCloner.Helpers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ObjectCloner.Actions
{
    public abstract class BaseCollectionCloningAction : ICloningAction
    {
        public object Clone(Cloner cloner, string propertyName, object valueFrom)
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

        protected abstract object GetItemValue(Cloner cloner, IList originalList, int index);
    }
}
