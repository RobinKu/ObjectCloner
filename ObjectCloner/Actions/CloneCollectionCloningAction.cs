using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ObjectCloner.Actions
{
    public class CloneCollectionCloningAction : BaseCollectionCloningAction
    {
        protected override object GetItemValue(CloneScope cloner, IList originelList, int index)
        {
            return originelList[index];
        }
    }
}
