using ObjectCloner.Actions;
using ObjectCloner.ObjectCreation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ObjectCloner.Annotations
{
    public class AnnotationMetadataCollector : IMetadataCollector
    {
        private IDictionary<Type, IEnumerable<CloningPropertyMetadata>> propertyMetadataPerType = new Dictionary<Type, IEnumerable<CloningPropertyMetadata>>();

        public CloningTypeMetadata CreateMetadataForType(Type type)
        {
            ClonableAttribute attribute = type.GetType().GetCustomAttributes(typeof(ClonableAttribute), true)
                .OfType<ClonableAttribute>()
                .SingleOrDefault();

            IFactory factory = GetFactory(attribute);

            CloningTypeMetadata metadata = new CloningTypeMetadata(type, factory, this);
            metadata.Clonable = attribute != null;

            return metadata;
        }

        private static IFactory GetFactory(ClonableAttribute attribute)
        {
            IFactory factory = null;

            if (attribute != null && attribute.FactoryType != null)
            {
                factory = (IFactory)Activator.CreateInstance(attribute.FactoryType);
            }

            if (factory == null)
            {
                factory = Factory.Default;
            }

            return factory;
        }

        public CloningPropertyMetadata CreateMetadataForProperty(Type type, string propertyName)
        {
            return GetPropertyMetadataForType(type).Single(md => md.PropertyName == propertyName);
        }

        public IEnumerable<string> GetPropertyNames(Type type)
        {
            return GetPropertyMetadataForType(type).Select(md => md.PropertyName);
        }

        private IEnumerable<CloningPropertyMetadata> GetPropertyMetadataForType(Type type)
        {
            IEnumerable<CloningPropertyMetadata> propertyMetadata;

            if (!this.propertyMetadataPerType.TryGetValue(type, out propertyMetadata))
            {
                propertyMetadata = this.ScanProperties(type).ToList();

                this.propertyMetadataPerType.Add(type, propertyMetadata);
            }

            return propertyMetadata;
        }

        private IEnumerable<CloningPropertyMetadata> ScanProperties(Type type)
        {
            IEnumerable<PropertyInfo> allProperties = type.GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

            IEnumerable<Tuple<PropertyInfo, CloningAttribute>> propertiesWithAttribute = this.GetPropertiesWithAttribute(allProperties).ToList();

            bool useExpliciteMode = this.UseExplicitMode(propertiesWithAttribute);

            foreach (Tuple<PropertyInfo, CloningAttribute> propAttr in propertiesWithAttribute)
            {
                PropertyInfo property = propAttr.Item1;
                CloningAttribute attribute = propAttr.Item2;

                string propertyName = property.Name;
                ICloningAction action = GetActionForProperty(property, attribute, useExpliciteMode);

                if (action != null)
                {
                    yield return new CloningPropertyMetadata(propertyName, action);
                }
            }
        }

        private static ICloningAction GetActionForProperty(PropertyInfo property, CloningAttribute attribute, bool useExpliciteMode)
        {
            ICloningAction action = null;

            if (attribute == null)
            {
                if (!useExpliciteMode)
                {
                    if (property.PropertyType.IsValueType)
                    {
                        action = new AdoptValueCloningAction();
                    }
                    else
                    {
                        action = new CloneValueCloningAction();
                    }
                }
            }
            else
            {
                action = attribute.Action;
            }

            return action;
        }

        private bool UseExplicitMode(IEnumerable<Tuple<PropertyInfo, CloningAttribute>> propertiesWithAttribute)
        {
            return propertiesWithAttribute
                .Select(pa => pa.Item2)
                .Where(a => a.HasAction)
                .Any();
        }

        private IEnumerable<Tuple<PropertyInfo, CloningAttribute>> GetPropertiesWithAttribute(IEnumerable<PropertyInfo> allProperties)
        {
            foreach (PropertyInfo property in allProperties)
            {
                CloningAttribute attribute;

                try
                {
                    attribute = property.GetCustomAttributes(typeof(CloningAttribute), true)
                        .OfType<CloningAttribute>()
                        .SingleOrDefault();
                }
                catch (InvalidOperationException ex)
                {
                    throw new InvalidOperationException(string.Format("Property {0} has more than 1 CloningAttribute.", property.Name), ex);
                }

                yield return Tuple.Create(property, attribute);
            }
        }
    }
}
