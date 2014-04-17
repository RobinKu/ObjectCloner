using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ObjectCloner.ObjectCreation
{
    public static class Factory
    {
        private static IFactory @default;

        public static IFactory Default
        {
            get
            {
                if (@default == null)
                {
                    @default = new DefaultConstructorFactory();
                }

                return @default;
            }
        }
    }
}
