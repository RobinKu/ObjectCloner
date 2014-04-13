using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ObjectCloner
{
    internal static class ReflectionHelper
    {
        internal static object GetPropertyValue(object obj, string propertyName)
        {
            PropertyInfo property = obj.GetType().GetProperty(propertyName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

            if (property == null)
            {
                throw new ArgumentException(string.Format("No property with name {0} exists on object of type {1}", propertyName, obj.GetType()));
            }
            else
            {
                return property.GetValue(obj, null);
            }
        }

        internal static void SetPropertyValue(object obj, string propertyName, object value)
        {
            PropertyInfo property = obj.GetType().GetProperty(propertyName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

            if (property == null)
            {
                throw new ArgumentException(string.Format("No property with name {0} exists on object of type {1}", propertyName, obj.GetType()));
            }
            else
            {
                property.SetValue(obj, value, null);
            }
        }
    }
}
