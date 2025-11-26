using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Serjbal.Utils;
using UnityEngine;


namespace Serjbal.App.MVVM
{
    public class Binder : MonoBehaviour, IBinding
    {
        private readonly List<IBinding> _bindings = new List<IBinding>();

        public void Bind(object source, object target)
        {
            var targetMembers = GetDataMembers(source);
            var sourceMembers = GetDataMembers(target);

            foreach (var sourceMember in sourceMembers)
            {
                foreach (var targetMember in targetMembers)
                {
                    if (sourceMember.Key == targetMember.Key)
                    {
                        CreateBinding(sourceMember, targetMember);
                    }
                }
            }

            Bind();
        }


        public void Bind()
        {
            if (_bindings.Count > 0)
                _bindings.ForEach(x => x.Bind());
        }

        public void Unbind()
        {
            if (_bindings.Count > 0)
                _bindings.ForEach(x => x.Unbind());
        }

        private void OnEnable() => Bind();
        private void OnDisable() => Unbind();

        private List<DataMember> GetDataMembers(object obj)
        {
            var members = new List<DataMember>();
            var type = obj.GetType();

            var properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.NonPublic)
                .Where(p => p.IsDefined(typeof(DataAttribute), true));

            foreach (var property in properties)
            {
                var attribute = property.GetCustomAttribute<DataAttribute>();
                var isReactiveProperty = IsReactiveProperty(property.PropertyType);
                members.Add(new DataMember
                {
                    Key = attribute.Key,
                    Type = property.PropertyType,
                    MemberType = MemberType.Property,
                    GetValue = () => property.GetValue(obj),
                    SetValue = SetPropertyValueAction(obj, property, isReactiveProperty),
                    IsReactiveProperty = IsReactiveProperty(property.PropertyType)
                });
            }

            var fields = type.GetFields(BindingFlags.Public | BindingFlags.Instance | BindingFlags.NonPublic)
                .Where(f => f.IsDefined(typeof(DataAttribute), true));

            foreach (var field in fields)
            {
                var attribute = field.GetCustomAttribute<DataAttribute>();
                var isReactiveProperty = IsReactiveProperty(field.FieldType);
                members.Add(new DataMember
                {
                    Key = attribute.Key,
                    Type = field.FieldType,
                    MemberType = MemberType.Field,
                    GetValue = () => field.GetValue(obj),
                    SetValue = SetFieldValueAction(obj, field, isReactiveProperty),
                    IsReactiveProperty = isReactiveProperty
                });
            }

            return members;
        }

        private Action<object> SetFieldValueAction(object obj, FieldInfo field, bool isReactiveProperty)
        {
            if (isReactiveProperty)
            {
                return value =>
                {
                    (field.GetValue(obj) as IReactiveProperty).Value = value;
                };
            } else
            {
                return value =>
                {
                    field.SetValue(obj, value);
                };
            }
        }

        private Action<object> SetPropertyValueAction(object obj, PropertyInfo field, bool isReactiveProperty)
        {
            if (isReactiveProperty)
            {
                return value =>
                {
                    (field.GetValue(obj) as IReactiveProperty).Value = value;
                };
            } else
            {
                return value =>
                {
                    field.SetValue(obj, value);
                };
            }
        }

        private bool IsReactiveProperty(Type type)
        {
            return type.IsGenericType && type.GetGenericTypeDefinition() == typeof(ReactiveProperty<>);
        }

        private void CreateBinding(DataMember sourceMember, DataMember targetMember)
        {
            if (!sourceMember.IsReactiveProperty && targetMember.IsReactiveProperty)
            {
                _bindings.Add(new ReactiveToPrimitiveBinding(sourceMember, targetMember));
                return;
            }

            if (sourceMember.IsReactiveProperty && targetMember.IsReactiveProperty)
            {
                _bindings.Add(new ReactiveToReactiveBinding(sourceMember, targetMember));
                return;
            }
        }

        public void Dispose()
        {
            Unbind();
            _bindings.Clear();
        }
    }


    public enum MemberType
    {
        Property,
        Field
    }

    public class DataMember
    {
        public string Key { get; set; }
        public Type Type { get; set; }
        public MemberType MemberType { get; set; }
        public Func<object> GetValue { get; set; }
        public Action<object> SetValue { get; set; }
        public bool IsReactiveProperty { get; set; }
    }

    public interface IBinding : IDisposable
    {
        void Bind();
        void Unbind();
    }

    public abstract class BindingBase : IBinding
    {
        public abstract void Bind();
        public abstract void Unbind();

        public virtual void Dispose()
        {
            Unbind();
        }
    }
}