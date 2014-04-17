using ObjectCloner.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ObjectCloner.Annotations
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public class StaticValueAttribute : CloningAttribute
    {
        private object value;

        public StaticValueAttribute(bool value)
            :base(new StaticValueAction(value))
        {
            this.value = value;
        }

        public StaticValueAttribute(byte value)
            : base(new StaticValueAction(value))
        {
            this.value = value;
        }

        public StaticValueAttribute(char value)
            : base(new StaticValueAction(value))
        {
            this.value = value;
        }

        public StaticValueAttribute(short value)
            : base(new StaticValueAction(value))
        {
            this.value = value;
        }

        public StaticValueAttribute(int value)
            : base(new StaticValueAction(value))
        {
            this.value = value;
        }

        public StaticValueAttribute(long value)
            : base(new StaticValueAction(value))
        {
            this.value = value;
        }

        public StaticValueAttribute(float value)
            : base(new StaticValueAction(value))
        {
            this.value = value;
        }

        public StaticValueAttribute(double value)
            : base(new StaticValueAction(value))
        {
            this.value = value;
        }

        public StaticValueAttribute(string value)
            : base(new StaticValueAction(value))
        {
            this.value = value;
        }

        public StaticValueAttribute(decimal value)
            : base(new StaticValueAction(value))
        {
            this.value = value;
        }

        public StaticValueAttribute(Type value)
            : base(new StaticValueAction(value))
        {
            this.value = value;
        }

        public StaticValueAttribute(object value)
            : base(new StaticValueAction(value))
        {
            this.value = value;
        }

        public object Value
        {
            get
            {
                return this.value;
            }
        }
    }
}
