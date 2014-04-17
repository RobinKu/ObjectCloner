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
using ObjectCloner.Actions;
using System;

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
