using System;
using System.Collections.Generic;

namespace Serjbal.Utils
{
    public class ReactiveProperty<T> : IReactiveProperty
    {
        public event Action<T> OnChanged;

        private T _value;

        public ReactiveProperty(T value) => _value = value;

        public ReactiveProperty() => _value = default;

        public T Value
        {
            get => _value;
            set
            {
                if (EqualityComparer<T>.Default.Equals(_value, value))
                    return;

                _value = value;
                OnChanged?.Invoke(_value);
            }
        }

        event Action<object> IReactiveProperty.OnChanged
        {
            add => OnChanged += value != null ? (Action<T>)(x => value(x)) : null;
            remove => OnChanged -= value != null ? (Action<T>)(x => value(x)) : null;
        }

        object IReactiveProperty.Value
        {
            get => Value;
            set => Value = (T)value;
        }

        Type IReactiveProperty.ValueType => typeof(T);
    }

    public interface IReactiveProperty
    {
        object Value { get; set; }
        Type ValueType { get; }
        event Action<object> OnChanged;
    }
}
