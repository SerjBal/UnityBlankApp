using System;
using Serjbal.Utils;

namespace Serjbal.App.MVVM
{

    public class ReactiveToPrimitiveBinding : BindingBase, IBinding
    {
        private IReactiveProperty _sourceGetValue;
        private Action<object> _targetSetValue;
        private bool _isEnabled;

        public ReactiveToPrimitiveBinding(DataMember source, DataMember target)
        {
            _sourceGetValue = (IReactiveProperty)source.GetValue.Invoke();
            _targetSetValue = target.SetValue;
        }

        public override void Bind()
        {
            if (_isEnabled) return;

            _sourceGetValue.OnChanged += _targetSetValue;
            _isEnabled = true;
        }

        public override void Unbind()
        {
            if (!_isEnabled) return;

            _sourceGetValue.OnChanged -= _targetSetValue;
            _isEnabled = false;
        }
    }
}
