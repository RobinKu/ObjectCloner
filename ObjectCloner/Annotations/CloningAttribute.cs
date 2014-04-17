using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ObjectCloner.Annotations
{
    public abstract class CloningAttribute : Attribute
    {
        ICloningAction action;

        public CloningAttribute(ICloningAction action)
        {
            this.action = action;
        }

        internal ICloningAction Action
        {
            get
            {
                return this.action;
            }
        }

        public bool HasAction
        {
            get
            {
                return this.Action != null;
            }
        }
    }
}
