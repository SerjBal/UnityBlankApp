using System;
using Serjbal.Utils;

namespace Serjbal.App.MVVM
{
    public class ReactiveToReactiveBinding : BindingBase, IBinding
    {
        private IReactiveProperty _sourceGetValue;
        private IReactiveProperty _targetGetValue;
        private Action<object> _sourceSetValue;
        private Action<object> _targetSetValue;
        private bool _isEnabled;

        public ReactiveToReactiveBinding(DataMember source, DataMember target)
        {
            _sourceGetValue = (IReactiveProperty)source.GetValue.Invoke();
            _targetGetValue = (IReactiveProperty)target.GetValue.Invoke();
            _sourceSetValue = source.SetValue;
            _targetSetValue = target.SetValue;
        }

        public override void Bind()
        {
            if (_isEnabled) return;

            _sourceGetValue.OnChanged += _targetSetValue;
            _targetGetValue.OnChanged += _sourceSetValue;
            _isEnabled = true;
        }

        public override void Unbind()
        {
            if (!_isEnabled) return;

            _sourceGetValue.OnChanged -= _targetSetValue;
            _targetGetValue.OnChanged -= _sourceSetValue;
            _isEnabled = false;
        }
    }
}
