using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ObjectCloner
{
    public class CloningPropertyMetaData
    {
        private readonly string propertyName;
        private readonly ICloningAction action;

        public CloningPropertyMetaData(string propertyName, ICloningAction action)
        {
            this.propertyName = propertyName;
            this.action = action;
        }

        public string PropertyName
        {
            get
            {
                return this.propertyName;
            }
        }

        public ICloningAction Action
        {
            get
            {
                return this.action;
            }
        }
    }
}
