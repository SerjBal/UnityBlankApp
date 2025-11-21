using System;
using System.Linq;
using System.Reflection;
using Serjbal.App.MVP;
using UnityEngine;

namespace Serjbal.App
{
    public class FieldsInjector
    {
        public void Inject(object from, object to)
        {
            if (from == null)
                throw new ArgumentNullException(nameof(from));
            if (to == null)
                throw new ArgumentNullException(nameof(to));


            FieldInfo[] vmFields = GetFields(to);
            var vmAttributes = vmFields.Select(x => GetDataAttribute(x)).ToArray();

            var vmTypeData = new TypeData
            {
                instance = to,
                fields = vmFields,
                attributes = vmAttributes
            };


            FieldInfo[] viewFields = GetFields(from);
            foreach (var viewField in viewFields)
            {
                var dataAttribute = GetDataAttribute(viewField);
                if (dataAttribute == null) continue;

                object viewFieldValue = viewField.GetValue(from);
                if (viewFieldValue == null) continue;

                FieldData viewTypeData = GetViewTypeData(from, viewField, dataAttribute);

                InjectIntoViewModel(vmTypeData, viewTypeData);
            }
        }

        private static FieldData GetViewTypeData(object viewInstance, FieldInfo viewField, DataAttribute dataAttribute)
        {
            return new FieldData
            {
                dataKey = dataAttribute.Value,
                value = viewField.GetValue(viewInstance),
                fieldType = viewField.FieldType
            };
        }

        private static void InjectIntoViewModel(TypeData vmTypeData, FieldData viewFieldData)
        {
            for (int i = 0; i < vmTypeData.fields.Length; i++)
            {
                var vmField = vmTypeData.fields[i];
                var vmAttribute = vmTypeData.attributes[i];

                if (vmAttribute == null) continue;
                if (vmAttribute.Value == viewFieldData.dataKey &&
                    vmField.FieldType.IsAssignableFrom(viewFieldData.fieldType))
                {
                    try
                    {
                        vmField.SetValue(vmTypeData.instance, viewFieldData.value);
                        // Debug.Log($"Successfully injected field '{viewFieldData.dataKey}' from View to ViewModel");
                        break;
                    } catch (Exception ex)
                    {
                        Debug.LogError($"Failed to inject field '{viewFieldData.dataKey}': {ex.Message}");
                    }
                }
            }
        }

        private static FieldInfo[] GetFields(object viewInstance)
        {
            return viewInstance
                .GetType()
                .GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
        }

        private static DataAttribute GetDataAttribute(FieldInfo field)
        {
            return field.GetCustomAttribute<DataAttribute>();
        }

        private struct TypeData
        {
            public object instance;
            public FieldInfo[] fields;
            public DataAttribute[] attributes;
        }

        private struct FieldData
        {
            public string dataKey;
            public object value;
            public Type fieldType;
        }
    }
}