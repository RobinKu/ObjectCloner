using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ObjectCloner.Actions
{
    public class CloneCollectionItemsCloneAction : CloneCollectionCloningAction
    {
        protected override object GetItemValue(CloneScope cloner, IList originelList, int index)
        {
            object itemValue = base.GetItemValue(cloner, originelList, index);

            return cloner.Clone(itemValue);
        }
    }
}
